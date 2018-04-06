using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour {

    public float speed;
    public Vector3 limit;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
    void Update () {
        if (Input.GetKey (KeyCode.A) && transform.position.x > -limit.x) {
            transform.Translate (Vector3.left * speed * Time.deltaTime);
        }
        if (Input.GetKey (KeyCode.D) && transform.position.x < limit.x) {
            transform.Translate (Vector3.right * speed * Time.deltaTime);
        }
        if (Input.GetKey (KeyCode.W) && transform.position.z < limit.z) {
            transform.Translate (Vector3.forward * speed * Time.deltaTime);
        }
        if (Input.GetKey (KeyCode.S) && transform.position.z > -limit.z) {
            transform.Translate (Vector3.back * speed * Time.deltaTime);
        }
	}
}
