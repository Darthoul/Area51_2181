using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownShooterMovement : MonoBehaviour {

    public float speed = 1;
    public float angularVelocity = 1;

    public GameObject bullet;

    public List<Color> colors = new List<Color> ();
    int colorIndex = 0;
    public int ColorIndex { get { return colorIndex; } }

    public SpriteRenderer spriteRenderer;
    public Transform sightDirection;
    public Transform sightObject;

    public LineRenderer sightLine;
    Vector3 mouseWorldPos;

    private GameObject wall;

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
        Cursor.visible = false;
        spriteRenderer.color = colors[colorIndex];
        axisList.Add (new Axis ("Horizontal", KeyCode.A, KeyCode.D));
        axisList.Add (new Axis ("Vertical", KeyCode.S, KeyCode.W));
        axisList.Add (new Axis ("Arrow_H", KeyCode.LeftArrow, KeyCode.RightArrow));
	}
	
	// Update is called once per frame
	void Update () {

        transform.Translate (Vector3.right * GetAxis ("Horizontal") * speed * Time.deltaTime, Space.World);
        transform.Translate (Vector3.up * GetAxis ("Vertical") * speed * Time.deltaTime, Space.World);
        //sightDirection.Rotate (Vector3.back * GetAxis ("Arrow_H") * angularVelocity * Time.deltaTime);

        mouseWorldPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
        mouseWorldPos.z = transform.position.z;
        Debug.DrawLine (transform.position, mouseWorldPos, Color.red);
        
        sightDirection.up = (mouseWorldPos - transform.position).normalized;
        sightLine.SetPositions (new Vector3[] { transform.position, transform.position + sightDirection.up * 3 });

        float scrollWheelValue = Input.GetAxis ("Mouse ScrollWheel");

        if (scrollWheelValue != 0) {
            MoveColor (-scrollWheelValue);
        }

        if (Input.GetMouseButtonDown (0)) {
            Shoot ();
        }
	}

    void LateUpdate () {
        sightObject.position = (Vector3.Distance (mouseWorldPos, transform.position) >= 1) ? mouseWorldPos : transform.position + sightDirection.up;


        if (wall) {
            Vector3 dir = wall.transform.position - transform.position;
            transform.position -= dir * 0.4f;
            wall = null;
        }
    }

    void Shoot () {
        SpriteRenderer tempRenderer = Instantiate (bullet, sightDirection.Find ("Cannon").position, sightDirection.rotation).GetComponent<SpriteRenderer> ();
        tempRenderer.color = spriteRenderer.color;
        Destroy (tempRenderer.gameObject, 2);
        TopDownCamMovement camera = Camera.main.GetComponent<TopDownCamMovement> ();
        camera.speed = 25;
        camera.impulseDirection = sightDirection.up;
    }

    void MoveColor (float moveValue) {
        moveValue *= 10;
        for (int i = 0; i < Mathf.Abs(moveValue); i++) {
            colorIndex += 1 * (int) Mathf.Sign (moveValue);
            if (colorIndex >= colors.Count) {
                colorIndex = 0;
            } else if (colorIndex < 0) {
                colorIndex = colors.Count - 1;
            }
        }
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
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Wall")) {
            wall = collision.gameObject;
        }
    }
}
