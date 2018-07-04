using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public float maxHP = 1f;
    public float currentHP;
    public PowerBallBehaviour currentPower;

    public float normalizedHP { get { return currentHP / maxHP; } }


    public void ModifyHP (float addValue) {
        currentHP = Mathf.Clamp (currentHP + addValue, 0, maxHP);
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
