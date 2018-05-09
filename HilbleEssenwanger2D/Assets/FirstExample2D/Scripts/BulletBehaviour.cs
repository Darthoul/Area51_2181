using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour {

    public float speed = 1;
    SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
        spriteRenderer = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate (Vector3.up * speed * Time.deltaTime);
	}

    void OnTriggerEnter2D (Collider2D other) {
        SpriteRenderer otherRenderer = other.GetComponent<SpriteRenderer>();
        if (otherRenderer != null && other.CompareTag ("Block")) {
            int targetAmmount = (otherRenderer.color == spriteRenderer.color) ? 5 : 2;
            other.GetComponent<BlockEntity> ().DecreaseLife (targetAmmount);
            Destroy (gameObject);
        }
    }
}
