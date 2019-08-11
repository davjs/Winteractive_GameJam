using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryViewModel : MonoBehaviour {
    public Button WallButton;
    public Text WallCountText;
    public Text DoorCountText;
    public Button TurretButton;
    public Text TurretCountText;
    public Button DoorButton;
    public BuildCursor WallCursor;
    public BuildCursor DoorCursor;
    public BuildCursor TurretCursor;

    void Start() {
        Inventory.Wood.Changed += WoodOnChanged;
        Inventory.Doors.Changed += DoorsChanged;
        Inventory.Metal.Changed += MetalChanged;
    }
    
    private void MetalChanged(int newValue) {
        if (newValue > Inventory.TurretCost) {
            TurretButton.GetComponentInChildren<Text>().text = "Turret";
            TurretCountText.text = (newValue / Inventory.TurretCost).ToString();
        }
        else {
            TurretCountText.text = 0.ToString();
        }

    }
    
    private void DoorsChanged(int newValue) {
        if (newValue > 0) {
            DoorButton.GetComponentInChildren<Text>().text = "Door";
        }

        DoorCountText.text = newValue.ToString();
    }

    private void WoodOnChanged(int newValue) {
        if (newValue > 0) {
            WallButton.GetComponentInChildren<Text>().text = "Wall";
        }

        WallCountText.text = newValue.ToString();
    }

    public void WallClicked() {
        if (Inventory.Wood > 0) {
            Instantiate(WallCursor);
        }
    }
    
    public void DoorsClicked() {
        if (Inventory.Doors > 0) {
            Instantiate(DoorCursor);
        }
    }
    
    public void TurretClicked() {
        if (Inventory.Metal > Inventory.TurretCost) {
            var cursor = Instantiate(TurretCursor);
            cursor.cost = Inventory.TurretCost;
        }
    }
}