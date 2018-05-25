using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerMovement : MonoBehaviour {

    public float gravity;
    float verticalSpeed;
    public float horizontalSpeed = 1;
    public float jumpForce = 1;

    Vector3 leftNode { get { return transform.position - new Vector3 (0.5f, 1, 0); } }
    Vector3 rightNode { get { return transform.position + new Vector3 (0.5f, -1, 0); } }

    bool isGrounded;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        RaycastHit2D left = Physics2D.Raycast (leftNode, Vector3.down, 0.1f);
        RaycastHit2D right = Physics2D.Raycast (rightNode, Vector3.down, 0.1f);

        if (left || right) {
            isGrounded = true;
            verticalSpeed = 0;
            CheckReposition (new RaycastHit2D[] { left, right });
        } else { 
            isGrounded = false; 
        }

        if (!isGrounded) {
            verticalSpeed -= gravity * Time.deltaTime;
        } else {
            if (Input.GetKeyDown (KeyCode.Space)) {
                verticalSpeed = jumpForce;
                isGrounded = false;
            }
        }
        transform.Translate (Input.GetAxis ("Horizontal") * horizontalSpeed * Time.deltaTime, verticalSpeed * Time.deltaTime, 0);
	}

    void CheckReposition (RaycastHit2D[] nodeRays) {
        foreach (RaycastHit2D ray in nodeRays) {
            if (ray) {
                float distance = ray.collider.transform.localScale.y / 2;
                float difference = (leftNode.y - ray.collider.transform.position.y) - distance;
                if (difference != 0) {
                    transform.Translate (0, -difference, 0);
                }
                break;
            }
        }
    }

    void OnDrawGizmos () {
        Gizmos.DrawSphere (leftNode, 0.2f);
        Gizmos.DrawSphere (rightNode, 0.2f);
    }
}
