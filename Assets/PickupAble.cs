using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class PickupAble : MonoBehaviour {
    public string pickupType;
    public int n;

    private void OnTriggerEnter(Collider other) {
        var inventory = other.GetComponent<Inventory>();
        if (inventory != null) {
            inventory.Pickup(pickupType, n);
            Destroy(gameObject);
        }
    }
}