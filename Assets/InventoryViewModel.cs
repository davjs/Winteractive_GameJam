using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryViewModel : MonoBehaviour {
    public Button WallButton;
    public Text WallCountText;
    public Button TurretButton;
    public WallCursor WallCursor;

    void Start() {
        Inventory.Wood.Changed += WoodOnChanged;
    }

    private void WoodOnChanged(int newValue) {
        if (newValue > 0) {
            WallButton.GetComponentInChildren<Text>().text = "Wood";
        }

        WallCountText.text = newValue.ToString();
    }

    public void WallClicked() {
        if (Inventory.Wood > 0) {
            Instantiate(WallCursor);
        }
    }

    void Update() {
    }
}