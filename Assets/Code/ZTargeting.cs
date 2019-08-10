using System.Linq;
using UnityEngine;

public class ZTargeting : MonoBehaviour {
    public GameObject marker;
    public GameObject openMarker;
    public GameObject selectedMarker;
    public Transform playerMesh;
    private WasdMovement _wasdMovement;
    private Rigidbody _playerBody;
    private int _faceAwayFrames = 0;
    private GameObject _target;
    private bool zTargeting;
    private GameObject _possibleTarget;

    void Start() {
        var closeEnemies = Enemy.Enemies.OrderBy(x =>
            (x.transform.position - transform.position).magnitude
        );
        _possibleTarget = closeEnemies.FirstOrDefault()?.gameObject;
        _wasdMovement = GetComponent<WasdMovement>();
        _playerBody = GetComponent<Rigidbody>();
    }

    void Update() {
        if (zTargeting) {
            if (_target == null) {
                StopZTargeting();
            }
            else {
                ZTargetingMovement();
                marker.transform.position = _target.transform.position;
                var distance = (_target.transform.position - transform.position).magnitude;
                if (distance > 100.0f || Input.GetKeyDown(KeyCode.Space)) {
                    StopZTargeting();
                }
                else {
                    ZTargetingMovement();
                }
            }
        }

        if (!zTargeting) {
            var closeEnemies = Enemy.Enemies.OrderBy(x =>
                (x.transform.position - transform.position).magnitude
            );
            _possibleTarget = closeEnemies.FirstOrDefault()?.gameObject;
            if (Input.GetKeyDown(KeyCode.Space)) {
                StartZTargeting();
            }

            if (_possibleTarget != null) {
                marker.transform.position = _possibleTarget.transform.position;
                marker.SetActive(true);
            }
            else {
                marker.SetActive(false);
            }
        }
    }

    private void ZTargetingMovement() {
        var moveSpeed = _wasdMovement.speed;

        var toEnemyVector = (_target.transform.position - transform.position);
        var distanceToEnemy = toEnemyVector.magnitude;
        var directionToEnemy = toEnemyVector.normalized;
        var directionToEnemyEuler = Vector2.SignedAngle(Vector2.right, directionToEnemy);

        var stickDirection = Vector2.zero;

        if (Input.GetKey(KeyCode.A))
            stickDirection += Vector2.left;

        if (Input.GetKey(KeyCode.D))
            stickDirection += Vector2.right;

        if (Input.GetKey(KeyCode.W))
            stickDirection += Vector2.up;

        if (Input.GetKey(KeyCode.S))
            stickDirection += Vector2.down;

        var stickDirectionEuler = Vector2.SignedAngle(Vector2.right, stickDirection);
        if (stickDirection.magnitude > 0) {
            var diffBetweenDirections = Mathf.DeltaAngle(stickDirectionEuler, directionToEnemyEuler);
            Vector2 directionToMove;
            if (Mathf.Abs(diffBetweenDirections) < 90
                && Mathf.Abs(diffBetweenDirections) > 45
                && distanceToEnemy < 50.0f) {
                if (diffBetweenDirections > 0) {
                    directionToMove = VectorRelativeAngle(directionToEnemyEuler - 88); //+ stickDirection * 0.6f;
                }
                else {
                    directionToMove = VectorRelativeAngle(directionToEnemyEuler + 88); // + stickDirection * 0.6f;
                }

                _faceAwayFrames = 0;
            }
            else {
                directionToMove = stickDirection;
                _faceAwayFrames++;
            }

            _playerBody.AddForce(directionToMove.normalized * moveSpeed * 0.5f);
        }
        else {
            _faceAwayFrames = 0;
        }

        var faceEnemy = _faceAwayFrames < 30;
        if (faceEnemy) {
            WasdMovement.LastDirection = directionToEnemy;
            playerMesh.rotation = Quaternion.Euler(0, 0, directionToEnemyEuler);
        }
        else {
            WasdMovement.LastDirection = stickDirection;
            playerMesh.rotation = Quaternion.Euler(0, 0, stickDirectionEuler);
        }
    }

    private static Vector2 VectorRelativeAngle(float angleDegrees) {
        return new Vector2(Mathf.Cos(angleDegrees * Mathf.Deg2Rad), Mathf.Sin(angleDegrees * Mathf.Deg2Rad));
    }

    private void StartZTargeting() {
        _target = _possibleTarget;
        openMarker.SetActive(false);
        selectedMarker.SetActive(true);
        _wasdMovement.zTargeting = true;
        zTargeting = true;
    }

    private void StopZTargeting() {
        _possibleTarget = _target;
        _target = null;
        openMarker.SetActive(true);
        selectedMarker.SetActive(false);
        _wasdMovement.zTargeting = false;
        zTargeting = false;
    }
}