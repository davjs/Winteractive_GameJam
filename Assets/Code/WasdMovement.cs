using UnityEngine;

public class WasdMovement : MonoBehaviour
{
    public float speed = 10.0f;
    private Rigidbody _body;
    public bool zTargeting;
    private GameObject ClosestEnemy;
    public Transform PlayerMesh;
    
    void Start() {
        _body = GetComponent<Rigidbody>();
    }

    public static Vector3 LastDirection = Vector3.zero;

    void Update() {
        if (zTargeting) {
            return;
        }
        var moveSpeed = speed;

        var direction = Vector3.zero;
        
        if (Input.GetKey(KeyCode.A))
            direction+= Vector3.left;
        
        if (Input.GetKey(KeyCode.D))
            direction+= Vector3.right;
        
        if (Input.GetKey(KeyCode.W))
            direction+= Vector3.up;
        
        if (Input.GetKey(KeyCode.S))
            direction+= Vector3.down;

        if (direction.magnitude > 0) {
            _body.AddForce(direction.normalized * moveSpeed);
            LastDirection = direction;
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, -0.5f);
        PlayerMesh.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.right, direction));
    }
}
