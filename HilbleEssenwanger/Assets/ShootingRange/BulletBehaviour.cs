using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour {

    public float speed = 10;
    public float lifeSpan = 3;

	// Use this for initialization
	void Start () {
        Destroy (gameObject, lifeSpan);
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate (Vector3.forward * speed * Time.deltaTime);
	}
}
