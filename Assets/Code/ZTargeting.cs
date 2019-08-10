using System.Linq;
using UnityEngine;

public class ZTargeting : MonoBehaviour {
    private GameObject Target;
    private GameObject PossibleTarget;
    public GameObject Marker;
    public GameObject OpenMarker;
    public GameObject SelectedMarker;
    public Transform PlayerMesh;
    private WasdMovement WasdMovement;
    private Rigidbody PlayerBody;
    private int faceAwayFrames = 0;

    void Start() {
        var closeEnemies = Enemy.Enemies.OrderBy(x =>
            (x.transform.position - transform.position).magnitude
        );
        PossibleTarget = closeEnemies.FirstOrDefault()?.gameObject;
        WasdMovement = GetComponent<WasdMovement>();
        PlayerBody = GetComponent<Rigidbody>();
    }

    void Update() {
        if (Target == null) {
            var closeEnemies = Enemy.Enemies.OrderBy(x =>
                (x.transform.position - transform.position).magnitude
            );
            PossibleTarget = closeEnemies.FirstOrDefault()?.gameObject;
            if (Input.GetKeyDown(KeyCode.Space)) {
                StartZTargeting();
            }

            if (PossibleTarget != null) {
                Marker.transform.position = PossibleTarget.transform.position;
            }
        }
        else {
            ZTargetingMovement();
            Marker.transform.position = Target.transform.position;
            var distance = (Target.transform.position - transform.position).magnitude;
            if (distance > 100.0f || Input.GetKeyDown(KeyCode.Space)) {
                StopZTargeting();
            }
            else {
                ZTargetingMovement();
            }
        }
    }

    private void ZTargetingMovement() {
        var moveSpeed = WasdMovement.speed;

        var toEnemyVector = (Target.transform.position - transform.position);
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

                faceAwayFrames = 0;
            }
            else {
                directionToMove = stickDirection;
                faceAwayFrames++;
            }

            PlayerBody.AddForce(directionToMove.normalized * moveSpeed * 0.5f);
        }
        else {
            faceAwayFrames = 0;
        }

        var faceEnemy = faceAwayFrames < 30;
        if (faceEnemy) {
            WasdMovement.LastDirection = directionToEnemy;
            PlayerMesh.rotation = Quaternion.Euler(0, 0, directionToEnemyEuler);
        }
        else {
            WasdMovement.LastDirection = stickDirection;
            PlayerMesh.rotation = Quaternion.Euler(0, 0, stickDirectionEuler);
        }
    }

    private static Vector2 VectorRelativeAngle(float angleDegrees) {
        return new Vector2(Mathf.Cos(angleDegrees * Mathf.Deg2Rad), Mathf.Sin(angleDegrees * Mathf.Deg2Rad));
    }

    private void StartZTargeting() {
        Target = PossibleTarget;
        OpenMarker.SetActive(false);
        SelectedMarker.SetActive(true);
        WasdMovement.zTargeting = true;
    }

    private void StopZTargeting() {
        PossibleTarget = Target;
        Target = null;
        OpenMarker.SetActive(true);
        SelectedMarker.SetActive(false);
        WasdMovement.zTargeting = false;
    }
}