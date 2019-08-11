using System.Linq;
using UnityEngine;

public class BuildCursor : MonoBehaviour {
    public MeshRenderer Mesh;
    public GameObject toBuild;
    public float wallSize = 5;
    public string costType;
    public int cost = 1;


    private void Update() {
        if (Input.GetKeyDown(KeyCode.Q)) {
            Destroy(gameObject);
            return;
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            transform.Rotate(0, 0, 90);
            return;
        }

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var plane = new Plane(Vector3.forward, Vector3.zero);

        if (plane.Raycast(ray, out var distance)) {
            var hitPoint = ray.GetPoint(distance);
            hitPoint.z = transform.position.z;

            var roundedPosition = new Vector3(
                Mathf.Round(hitPoint.x / wallSize) * wallSize,
                Mathf.Round(hitPoint.y / wallSize) * wallSize,
                hitPoint.z);
            transform.position = roundedPosition;

            var collisions = Physics.OverlapBox(roundedPosition,
                new Vector3(wallSize * 0.4f, wallSize * 0.4f, wallSize), Quaternion.identity);
            if (collisions.Any(x => !x.CompareTag("Ground"))) {
                Mesh.material.color = Color.red;
            }
            else {
                Mesh.material.color = Color.yellow;

                if (Input.GetMouseButton(0)) {
                    Instantiate(toBuild, roundedPosition, transform.rotation);
                    Inventory.Reduce(costType, cost);
                    if (!Inventory.Has(costType, cost)) {
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}