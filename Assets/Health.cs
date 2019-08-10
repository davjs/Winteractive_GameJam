using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour {
    public int Max = 100;
    private int current;
    public Transform HealthBar;

    private Vector3 healthBarOriginalScale;

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

    // Start is called before the first frame update
    void Start() {
        healthBarOriginalScale = HealthBar.localScale;
        Current = Max;
    }

    void Update() {
    }
}