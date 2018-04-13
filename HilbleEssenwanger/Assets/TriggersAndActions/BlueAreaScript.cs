using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueAreaScript : MonoBehaviour {

    public Light targetLight;

	void OnTriggerEnter (Collider other) {
        Debug.Log ("Enter");
        if (other.CompareTag ("Player")) {
            targetLight.enabled = true;
        }
	}
}
