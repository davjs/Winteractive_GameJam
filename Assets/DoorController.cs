using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class DoorController : MonoBehaviour {
    public Transform Door;
    public float MoveFrames;
    public float DeltaPerFrame;
    public int waitTimeMs;
    public bool Open;
    public Vector3 Axis;

    private async void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player") && !Open) {
            Open = true;
            for (int i = 0; i < MoveFrames; i++) {
                await Task.Delay(waitTimeMs);
                Door.Translate(Axis * DeltaPerFrame);
            }
        }
    }
    
    private async void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Player") && Open) {
            Open = false;
            for (int i = 0; i < MoveFrames; i++) {
                await Task.Delay(waitTimeMs);
                Door.Translate(-Axis * DeltaPerFrame);
            }
        }
    }

}
