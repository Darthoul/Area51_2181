using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedAreaScript : MonoBehaviour {

    public Light targetLight;

    void OnTriggerExit (Collider other) {
        Debug.Log ("Exit");
        if (other.CompareTag ("Player")) {
            targetLight.enabled = false;
        }
    }
}
