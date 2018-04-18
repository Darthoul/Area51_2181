using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour {

    public float angleVelocity = 1;
    public float speed = 1;

    public AudioClip gunSound;
    public Transform gun;
    public GameObject playerBullet;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate (Vector3.up * Input.GetAxis ("Mouse X") * angleVelocity * Time.deltaTime);
        transform.Translate (Vector3.right * Input.GetAxis ("Horizontal") * speed * Time.deltaTime);
        transform.Translate (Vector3.forward * Input.GetAxis ("Vertical") * speed * Time.deltaTime);

        if (Input.GetMouseButtonDown (0)) {
            gun.GetComponent<AudioSource> ().PlayOneShot (gunSound);
            Instantiate (playerBullet, gun.Find ("Cannon").position, transform.rotation);
            Debug.Log ("BANG!");
        }
	}
}
