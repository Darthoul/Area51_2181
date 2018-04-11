using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour {

    public float speed;
    public Vector3 limit;
    Rigidbody playerRB;
    Vector3 initialPos;

	// Use this for initialization
	void Start () {
        initialPos = transform.position;
        playerRB = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
    void Update () {
        if (Input.GetKey (KeyCode.A) && transform.position.x > -limit.x) {
            //transform.Translate (Vector3.left * speed * Time.deltaTime);
            playerRB.MovePosition (transform.position + (Vector3.left * speed * Time.deltaTime));
        }
        if (Input.GetKey (KeyCode.D) && transform.position.x < limit.x) {
            playerRB.MovePosition (transform.position + (Vector3.right * speed * Time.deltaTime));
        }
        if (Input.GetKey (KeyCode.W) && transform.position.z < limit.z) {
            playerRB.MovePosition (transform.position + (Vector3.forward * speed * Time.deltaTime));
        }
        if (Input.GetKey (KeyCode.S) && transform.position.z > -limit.z) {
            playerRB.MovePosition (transform.position + (Vector3.back * speed * Time.deltaTime));
        }
	}

    public void ResetPosition () {
        transform.position = initialPos;
    }
}
