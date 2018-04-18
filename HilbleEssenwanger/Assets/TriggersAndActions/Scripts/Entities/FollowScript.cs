using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowScript : MonoBehaviour {

    public float speed = 1;
    public Transform target;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (SpawnArea.lightIndicator.enabled) {
            transform.position = Vector3.MoveTowards (transform.position, target.position, speed * Time.deltaTime);
        }
	}
}
