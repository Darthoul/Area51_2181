using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HangmanScript : MonoBehaviour {

    public string palabra;
    string palabraEscondida;

    public Text outputText;
    public InputField inputText;

    public AudioClip successSound;
    public AudioClip failSound;
    public AudioClip winSound;
    public AudioSource camSource;

    bool endHangman = false;

	// Use this for initialization
	void Start () {
        inputText.Select ();
        foreach (char c in palabra) {
            palabraEscondida += "*";
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (!endHangman) {
            RunHangman ();
        }
	}

    void RunHangman () {
        if (outputText.text != palabraEscondida) {
            outputText.text = palabraEscondida;
        }
        if (Input.GetKeyDown (KeyCode.Return) && !string.IsNullOrEmpty (inputText.text)) {
            string letra = inputText.text.Substring (0, 1);
            if (palabra.Contains (letra)) {
                string palabraTemporal = "";
                for (int i = 0; i < palabra.Length; i++) {
                    if (palabra[i] == letra[0]) {
                        palabraTemporal += letra;
                    } else {
                        palabraTemporal += palabraEscondida[i];
                    }
                }
                palabraEscondida = palabraTemporal;
                camSource.PlayOneShot (successSound);
            } else {
                camSource.PlayOneShot (failSound);
            }
            inputText.text = "";
            inputText.Select ();
            inputText.ActivateInputField ();
        }
        if (palabraEscondida == palabra) {
            camSource.PlayOneShot (winSound);
            outputText.text = "FELICIDADES, GANASTE!";
            inputText.gameObject.SetActive (false);
            endHangman = true;
        }
    }
}
