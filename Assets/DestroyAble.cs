using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class DestroyAble : MonoBehaviour {
    public float health = 100;
    public GameObject Drops;
    public int DropCount = 1;
    public MeshRenderer mesh;

    public async void Damage(float dmg) {
        health -= dmg;
        if (health <= 0) {
            for (int i = 0; i < DropCount; i++) {
                Instantiate(Drops, new Vector3(transform.position.x + Random.value * 5, transform.position.y + Random.value * 5, -0.5f), Quaternion.identity);
            }
            Destroy(gameObject);
        }
        else if (mesh) {
            var oldColor = mesh.material.color;
            mesh.material.color = Color.red;
            await Task.Delay(15);
            mesh.material.color = oldColor;
        }
    }
}