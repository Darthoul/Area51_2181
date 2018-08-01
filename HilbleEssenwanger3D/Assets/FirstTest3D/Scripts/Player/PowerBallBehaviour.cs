using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBallBehaviour : MonoBehaviour {

    public PlatformMovement3D activePlayer;
    public Collider triggerArea;
    public Collider damageArea;
    public readonly string containerName = "PowerContainer";
    public readonly Vector3 idlePoint = new Vector3 (0.5f, 0.75f, 0f);
    public readonly Vector3 center = new Vector3 (0f, 0.75f, 0f);

    public string powerName;

    bool waitForNextAction = false;
    public bool isWaiting { get { return waitForNextAction; }}

	// Use this for initialization
	void Start () {
        if (!activePlayer) {
            triggerArea.enabled = true;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AssignActivePlayer (PlatformMovement3D targetPlayer) {
        activePlayer = targetPlayer;
        transform.SetParent (activePlayer.transform.Find(containerName));
        //transform.localPosition = idlePoint;
        transform.rotation = targetPlayer.transform.rotation;
        triggerArea.enabled = false;
        GetComponent<Animator> ().SetFloat ("idleSpeed", 0.5f);
    }

    public void SetAlpha (float alpha) {
        Color color = GetComponent<Renderer> ().material.color;
        color.a = alpha;
        GetComponent<Renderer> ().material.color = color;
    }

    public void AttackRoundAbout () {
        waitForNextAction = true;
        transform.parent.localPosition = center;
        GetComponent<Animator> ().SetTrigger ("Roundabout");
        damageArea.enabled = true;
    }
    public void AttackLongShot () {
        waitForNextAction = true;
        transform.parent.localPosition = center + (Vector3.forward * 0.25f);
        GetComponent<Animator> ().enabled = false;
        StartCoroutine (LongShotRoutine (transform.position + (Vector3.forward * 7.25f)));
        damageArea.enabled = true;
    }
    IEnumerator LongShotRoutine (Vector3 targetPoint) {
        Vector3 origin = transform.position;
        SetAlpha (1f);
        while (transform.position != targetPoint) {
            transform.position = Vector3.MoveTowards (transform.position, targetPoint, 10.5f * Time.deltaTime);
            yield return null;
        }

        while (transform.position != origin) {
            transform.position = Vector3.MoveTowards (transform.position, origin, 8.5f * Time.deltaTime);
            yield return null;
        }
        SetAlpha (0.25f);
        GetComponent<Animator> ().enabled = true;
        ResetPoint ();
        yield return null;
    }

    public void ResetPoint () {
        transform.parent.localPosition = idlePoint;
        waitForNextAction = false;
        damageArea.enabled = false;
    }

    public bool Compare (PowerBallBehaviour other) {
        return this == other;
    }

    void OnTriggerEnter (Collider other) {
        if (other.CompareTag("Damageable")) {
            Debug.Log (powerName.Replace ("Ball", ""));
            other.GetComponent<DamageableObject> ().TakeDamage (powerName.Replace ("Ball", ""));
        }
    }
}
