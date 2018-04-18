using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenAreaScript : MonoBehaviour {

    public Light targetLight;
    public bool runUpdate;
    float timer = 0;

    void OnTriggerStay (Collider other) {
        //Debug.Log ("Stay");
        if (other.CompareTag ("Player") && !runUpdate) {
            runUpdate = true;
        }
    }

    void OnTriggerExit (Collider other) {
        if (other.CompareTag ("Player")) {
            runUpdate = false;
        }
    }

    void Update () {
        if (runUpdate) {
            timer += Time.deltaTime;
            if (timer >= 1) {
                targetLight.enabled = !targetLight.enabled;
                timer = 0;
            }
        }
    }
}
