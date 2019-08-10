using System;
using System.Collections;
using UnityEngine;


public class Inventory : MonoBehaviour
{
    public static Observable<int> Wood = new Observable<int>(0);
    public static Observable<int> Turrets = new Observable<int>(0);

    public void Pickup(string itemName, int n) {
        if (itemName == "Wood") {
            Wood.Set(Wood + n);
        }
    }
}
