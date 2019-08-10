using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public static List<Enemy> Enemies = new List<Enemy>();
    public int health = 3;

    private void Start() {
        Enemies.Add(this);
    }

    private void OnDestroy() {
        Enemies.Remove(this);
    }
}