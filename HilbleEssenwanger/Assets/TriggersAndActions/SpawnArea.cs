using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArea : MonoBehaviour {

    public GameObject objectToSpawn;
    public bool spawnPossible = true;
    float timer = 0;

    void OnTriggerStay (Collider other) {
        if (other.CompareTag ("Follower")) {
            spawnPossible = false;
        }
    }

    void Update () {
        if (spawnPossible) {
            timer += Time.deltaTime;
            if (timer >= 1) {
                Instantiate (objectToSpawn, transform.position, Quaternion.identity);
                timer = 0;
            }
        }
    }
}
