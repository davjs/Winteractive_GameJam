using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prefabs : MonoBehaviour {
    public Material HurtMaterial;
    public static Prefabs Get;
    
    
    void Start() {
        Get = this;
    }
}
