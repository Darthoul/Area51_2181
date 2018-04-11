using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour {

	void OnTriggerEnter (Collider other) {
        Debug.Log ("Entre en Trigger con " + other);
        if (other.CompareTag("Player")) {
            other.GetComponent<Movimiento> ().ResetPosition ();
        }
	}
}
