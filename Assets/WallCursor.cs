using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WallCursor : MonoBehaviour {
    public MeshRenderer Mesh;
    public GameObject Wall;
    public float wallSize = 5;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Q)) {
            Destroy(gameObject);
            return;
        }
        
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var plane = new Plane(Vector3.forward, Vector3.zero);
        float distance;

        if (plane.Raycast(ray, out distance)) {
            var hitPoint = ray.GetPoint(distance);
            hitPoint.z = transform.position.z;

            var roundedPosition = new Vector3(
                Mathf.Round(hitPoint.x / wallSize) * wallSize,
                Mathf.Round(hitPoint.y / wallSize) * wallSize,
                hitPoint.z);
            transform.position = roundedPosition;

            var collisions = Physics.OverlapBox(roundedPosition, new Vector3(wallSize  * 0.4f, wallSize * 0.4f, wallSize), Quaternion.identity);
            if (collisions.Any(x => !x.CompareTag("Ground"))) {
                Mesh.material.color = Color.red;
            }
            else {
                Mesh.material.color = Color.yellow;
                
                if (Input.GetMouseButton(0)) {
                    Instantiate(Wall, roundedPosition, Quaternion.identity);
                    Inventory.Wood.Set(Inventory.Wood -1);
                    if (Inventory.Wood <= 0) {
                        Destroy(gameObject);
                    }
                }
            }

        }
    }
}