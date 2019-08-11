using System;
using System.Collections;
using UnityEngine;


public class Inventory : MonoBehaviour {
    public static int TurretCost = 5;
    public static Observable<int> Wood = new Observable<int>(0);
    public static Observable<int> Doors = new Observable<int>(0);
    public static Observable<int> Metal = new Observable<int>(0);
    public Transform LaserSword;

    public static bool Has(string type, int n) {
        if (type == "Wood") {
            return Wood > n;
        }

        if (type == "Door") {
            return Doors > n;
        }

        if (type == "Metal") {
            return Metal > n;
        }

        return false;
    }

    public static void Reduce(string type, int cost) {
        if (type == "Wood") {
            Wood.Set(Wood - cost);
        }

        if (type == "Door") {
            Doors.Set(Doors - cost);
        }

        if (type == "Metal") {
            Metal.Set(Metal - cost);
        }
    }

    public void Pickup(string itemName, int n) {
        if (itemName == "Wood") {
            Wood.Set(Wood + n);
        }

        if (itemName == "Glass") {
            Doors.Set(Doors + 1);
        }

        if (itemName == "Metal") {
            Metal.Set(Metal + n);
        }

        if (itemName == "LaserSword") {
            var player = GameObject.FindWithTag("Player");
            var meleeWeapon = player.GetComponent<MeleeWeapon>();
            meleeWeapon.Weapon = LaserSword;
        }
    }
}