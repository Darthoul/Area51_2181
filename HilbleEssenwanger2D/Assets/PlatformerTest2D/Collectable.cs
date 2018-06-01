using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collectable : MonoBehaviour {

    public static int count;

	// Use this for initialization
	void Start () {
        count++;
	}
	
	// Update is called once per frame
    void OnTriggerEnter2D (Collider2D other) {
        if (other.CompareTag("Player")) {
            count--;
        }

        int currentIndex = SceneManager.GetActiveScene ().buildIndex;
        if (CheckCount ()) {
            if (currentIndex < SceneManager.sceneCountInBuildSettings - 1) {
                SceneManager.LoadScene (currentIndex + 1);
            }else {
                SceneManager.LoadScene ("MainMenu");
            }
        }
        Destroy (gameObject);
	}

    bool CheckCount () {
        return count == 0;
    }

    public static void ResetCount () {
        count = 0;
    }
}
