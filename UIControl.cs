using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour {

	public Text debugText;
	public int border = 10;
	public float camSpeed = 2f;

	private Vector2 boxOrigin;
	private Vector2 boxEnd;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		debugText.text = Input.mousePosition.ToString();

		if (!Input.GetMouseButton(0)) {
			if (CheckBorder (0, Input.mousePosition.y)) {
				debugText.text += " Bottom";
				//Mover Abajo
				Camera.main.transform.Translate (Vector3.down * camSpeed * Time.deltaTime);
			} else if (CheckBorder(Screen.height, Input.mousePosition.y)) {
				debugText.text += " Top";
				//Mover Arriba
				Camera.main.transform.Translate (Vector3.up * camSpeed * Time.deltaTime);
			}

			if (CheckBorder(0, Input.mousePosition.x)) {
				debugText.text += " Left";
				//Mover Izquierda
			} else if (CheckBorder(Screen.width, Input.mousePosition.x)) {
				debugText.text += " Right";
				//Mover Derecha
			}
		} else {
			
		}

		if (Input.GetMouseButtonUp(0)) {
			boxOrigin = Vector2.zero;
			boxEnd = Vector2.zero;
		}
	}

	bool CheckBorder (float limit, float targetValue) {
		float difference = ((limit - border) <= 0) ? border : -border;
		if (difference > limit) {
			return targetValue > limit && targetValue < limit + difference;
		} else {
    		return targetValue < limit && targetValue > limit + difference;
		}
	}
}
