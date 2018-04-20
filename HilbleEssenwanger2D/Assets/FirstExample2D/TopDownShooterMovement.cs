using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownShooterMovement : MonoBehaviour {

    public float speed = 1;
    public float angularVelocity = 1;

    public GameObject bullet;

    public List<Color> colors = new List<Color> ();
    int colorIndex = 0;

    public SpriteRenderer spriteRenderer;

    class Axis {
        public string name;
        public KeyCode negative;
        public KeyCode positive;

        public Axis (string _name, KeyCode _negative, KeyCode _positive) {
            name = _name;
            negative = _negative;
            positive = _positive;
        }
    }

    List<Axis> axisList = new List<Axis> ();

	// Use this for initialization
	void Start () {
        spriteRenderer.color = colors[colorIndex];
        axisList.Add (new Axis ("Horizontal", KeyCode.A, KeyCode.D));
        axisList.Add (new Axis ("Vertical", KeyCode.S, KeyCode.W));
        axisList.Add (new Axis ("Arrow_H", KeyCode.LeftArrow, KeyCode.RightArrow));
	}
	
	// Update is called once per frame
	void Update () {

        transform.Translate (Vector3.right * GetAxis ("Horizontal") * speed * Time.deltaTime, Space.World);
        transform.Translate (Vector3.up * GetAxis ("Vertical") * speed * Time.deltaTime, Space.World);
        transform.Rotate (Vector3.back * GetAxis ("Arrow_H") * angularVelocity * Time.deltaTime);

        if (Input.GetKeyDown (KeyCode.E)) {
            MoveColor ();
        }

        if (Input.GetKeyDown (KeyCode.Space)) {
            Shoot ();
        }
	}

    void Shoot () {
        SpriteRenderer tempRenderer = Instantiate (bullet, transform.Find ("Cannon").position, transform.rotation).GetComponent<SpriteRenderer>();
        tempRenderer.color = spriteRenderer.color;
        Destroy (tempRenderer.gameObject, 2);
    }

    void MoveColor () {
        colorIndex = (colorIndex >= colors.Count - 1) ? 0 : colorIndex + 1;
        spriteRenderer.color = colors[colorIndex];
    }

    int GetAxis (string axisName) {
        Axis axis = axisList.Find (target => target.name == axisName);
        int axisValue = 0;
        if (Input.GetKey (axis.negative)) {
            axisValue += -1;
        }
        if (Input.GetKey (axis.positive)) {
            axisValue += 1;
        }
        return axisValue;
    }

    void OnTriggerEnter2D (Collider2D other) {
        if (other.CompareTag ("Block")) {
            Debug.Log ("Block collision!");
        }
    }
}
