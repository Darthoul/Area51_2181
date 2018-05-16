using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSprite : MonoBehaviour {

    private SpriteRenderer spriteRenderer;
    public Sprite spriteUp;
    public Sprite spriteDown;
    public Sprite spriteLeft;
    public Sprite spriteRight;

	// Use this for initialization
	void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = spriteUp;
	}
	
	// Update is called once per frame
	void Update () {
		
        if (Input.GetKeyDown(KeyCode.A)) {
            spriteRenderer.sprite = spriteLeft;
        }

        if (Input.GetKeyDown(KeyCode.D)) {
            spriteRenderer.sprite = spriteRight;
        }

        if (Input.GetKeyDown(KeyCode.W)) {
            spriteRenderer.sprite = spriteUp;
        }

        if (Input.GetKeyDown(KeyCode.S)) {
            spriteRenderer.sprite = spriteDown;
        }

	}
}
