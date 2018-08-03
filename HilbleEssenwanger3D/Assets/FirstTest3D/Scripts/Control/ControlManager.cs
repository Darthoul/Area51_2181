using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlManager : MonoBehaviour {

    static public ControlManager instance;

    public MovementControl currentMovement;
    MovementControl playerDefault;

    void Awake () {
        if (instance == null) {
            instance = this;
        }
        currentMovement.Awake (transform);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetFromPlayerScript (ref PlayerScript playerScript) {
        playerDefault = playerScript.GetComponent<PlatformMovement3D> ();
    }
}
