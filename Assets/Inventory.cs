using System;
using System.Collections;
using UnityEngine;


public class Inventory : MonoBehaviour
{
    public static Observable<int> Wood = new Observable<int>(0);
    public static Observable<int> Doors = new Observable<int>(10);
    public static Observable<int> Turrets = new Observable<int>(0);

    public static bool Has(string type) {
        if (type == "Wood") {
            return Wood > 0;
        }
        if (type == "Door") {
            return Doors > 0;
        }

        return false;
    }

    public static void Reduce(string type) {
        if (type == "Wood") {
            Wood.Set(Wood-1);   
        }
        if (type == "Door") {
            Doors.Set(Doors-1);   
        }
    }
    
    public void Pickup(string itemName, int n) {
        if (itemName == "Wood") {
            Wood.Set(Wood + n);
        }

        if (itemName == "Glass") {
            Doors.Set(Doors + 1);
        }
    }
}
