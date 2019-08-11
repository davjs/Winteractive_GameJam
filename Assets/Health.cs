using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour {
    public int Max = 100;
    private int current;
    public Transform HealthBar;

    private Vector3 healthBarOriginalScale;
    public MeshRenderer Body;
    private Material OriginalMaterial;

    public int Current {
        get { return current; }
        set {
            current = value;
            HealthBar.localScale = new Vector3(
                (float) current / Max * healthBarOriginalScale.x,
                healthBarOriginalScale.y,
                healthBarOriginalScale.z
            );
            if (value <= 0) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    public async void Damage(int dmg) {
        Current -= dmg;
        Body.material = Prefabs.Get.HurtMaterial;
        await Task.Delay(50);
        if (Body != null) {
            Body.material = OriginalMaterial;
        }
    }

    void Start() {
        healthBarOriginalScale = HealthBar.localScale;
        Current = Max;
        OriginalMaterial = Body.material;
    }
}