using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryViewModel : MonoBehaviour {
    public Button WallButton;
    public Text WallCountText;
    public Text DoorCountText;
    public Button DoorButton;
    public BuildCursor WallCursor;
    public BuildCursor DoorCursor;

    void Start() {
        Inventory.Wood.Changed += WoodOnChanged;
        Inventory.Doors.Changed += DoorsChanged;
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

    void Update() {
    }
}