using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionEventWrapper : MonoBehaviour {

    PlatformMovement3D platformMovement;

    void Awake () {
        platformMovement = GetComponent<PlatformMovement3D> ();
    }

    void OnTriggerEnter (Collider other) {
        platformMovement.OnTriggerEnter (other);
    }

    void OnCollisionStay (Collision collision) {
        platformMovement.OnCollisionStay (collision);
    }

    void OnCollisionExit (Collision collision) {
        platformMovement.OnCollisionExit (collision);
    }
}
