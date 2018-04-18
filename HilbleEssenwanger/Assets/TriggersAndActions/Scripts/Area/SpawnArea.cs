using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArea : MonoBehaviour {

    public GameObject objectToSpawn;
    public Transform objectToFollow;
    public bool spawnPossible = true;

    public Transform lightBox;
    static public Light lightIndicator;

    float timer = 0;

    void Start () {
        lightIndicator = lightBox.Find ("Point light").GetComponent<Light> ();
    }

    void OnTriggerStay (Collider other) {
        if (other.CompareTag ("Follower")) {
            spawnPossible = false;
        }
    }

    void OnTriggerExit (Collider other) {
        if (other.CompareTag ("Follower")) {
            spawnPossible = true;
        }
    }

    void Update () {
        if (spawnPossible) {
            timer += Time.deltaTime;
            if (timer >= 1) {
                GameObject spawnedObject = Instantiate (objectToSpawn, transform.position, Quaternion.identity);
                spawnedObject.transform.Translate (Vector3.down);
                spawnedObject.GetComponent<FollowScript> ().target = objectToFollow;
                spawnedObject.GetComponent<FollowScript> ().speed = Random.Range (1f, 5.5f);
                timer = 0;
            }
        }
    }
}
