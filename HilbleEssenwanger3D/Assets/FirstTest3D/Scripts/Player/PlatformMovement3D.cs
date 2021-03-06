﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement3D : MovementControl {

    public float angularSpeed = 75;
    public float movementSpeed = 8;
    public Rigidbody rigidbodyComponent;
    Vector3 movement;
    Quaternion rotation;

    public Animator animatorController;
    public PlayerScript playerScript;

    bool grounded;

    List<Collider> groundCollection;

    public override void Awake (Transform transform) {
        rigidbodyComponent = transform.GetComponent<Rigidbody> ();
        animatorController = transform.GetComponent<Animator> ();
        playerScript = transform.GetComponent<PlayerScript> ();
    }

	// Use this for initialization
	public override void Start () {
        groundCollection = new List<Collider> ();
	}
	
	// Update is called once per frame
    public override void FixedUpdate (ref Transform transform) {

        movement = transform.position;
        rotation = rigidbodyComponent.rotation;
        float horizontalMovement = Input.GetAxis ("Horizontal");
        float verticalMovement = Input.GetAxis ("Vertical");

        animatorController.SetFloat ("forwardSpeed", NormalizeMovement (verticalMovement));

        if (Input.GetKey(KeyCode.I)) {
            rotation *= Quaternion.Euler (Vector3.up * -angularSpeed * Time.fixedDeltaTime);
        }
        if (Input.GetKey (KeyCode.O)) {
            rotation *= Quaternion.Euler (Vector3.up * angularSpeed * Time.fixedDeltaTime);
        }
        if (horizontalMovement != 0) {
            movement += transform.right * movementSpeed * horizontalMovement * Time.fixedDeltaTime;
        }
        if (verticalMovement != 0) {
            movement += transform.forward * movementSpeed * verticalMovement * Time.fixedDeltaTime;
        }

        rigidbodyComponent.MovePosition (movement);
        rigidbodyComponent.MoveRotation (rotation);
	}

    public override void Update (ref Transform transform) {
        if (Input.GetKeyDown(KeyCode.Space) && grounded) {
            rigidbodyComponent.AddForce (Vector3.up * 5f, ForceMode.Impulse);
            playerScript.ModifyHP (-20);
        } else if (Input.GetKeyDown (KeyCode.J) && !playerScript.currentPower.isWaiting) {
            Attack ();
        }
    }

    void Attack() {
        if (!playerScript.isSightMode) {
            playerScript.currentPower.AttackRoundAbout ();
        } else {
            playerScript.currentPower.AttackLongShot ();
        }
    }

    float NormalizeMovement (float targetMovement) {
        return (targetMovement + 1f) / 2f;
    }

    public void OnTriggerEnter (Collider other) {
        if (other.CompareTag("Power")) {
            PowerBallBehaviour targetPower = other.GetComponent<PowerBallBehaviour> ();
            if (playerScript.currentPower != null || playerScript.currentPower != targetPower) {
                if (playerScript.currentPower != null && playerScript.currentPower != targetPower) {
                    Object.Destroy (playerScript.currentPower.gameObject);
                }
                playerScript.currentPower = targetPower;
                targetPower.AssignActivePlayer (this);
                QuestManager.instance.Check ("obtain", targetPower.powerName);
            }
        }
    }

    public void OnCollisionStay (Collision collision) {
        if (!groundCollection.Contains (collision.collider)) {
            foreach (ContactPoint contact in collision.contacts) {
                Debug.DrawRay (contact.point, contact.normal * 5f, Color.red, 1f);
                if (Vector3.Dot (contact.normal, Vector3.up) > 0.75f) {
                    Debug.Log ("SHOULD BE GROUNDED!");
                    grounded = true;
                    animatorController.SetBool ("isGrounded", grounded);
                    groundCollection.Add (collision.collider);
                    break;
                }
            }
        }
    }

    public void OnCollisionExit (Collision collision) {
        if (groundCollection.Contains(collision.collider)) {
            groundCollection.Remove (collision.collider);
        }
        if (groundCollection.Count <= 0) { 
            grounded = false;
            animatorController.SetBool ("isGrounded", grounded);
        }
    }
}
