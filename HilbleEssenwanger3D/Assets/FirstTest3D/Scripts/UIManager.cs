using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    static public UIManager instance;

    public Image hpBar;
    public PlayerScript playerScript;
    public Gradient barColors;
    public Image restartPanel;
    public float restartSpeed;

    bool isLoading = false;

    void Awake () {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad (gameObject);
        } else {
            DestroyImmediate (gameObject);
        }
        
    }

	// Use this for initialization
	void Start () {
        if (playerScript == null) {
            playerScript = FindPlayerInstance ();
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (playerScript != null && hpBar.fillAmount != playerScript.normalizedHP) {
            float delta = Mathf.Abs (hpBar.fillAmount - playerScript.normalizedHP);
            if (delta < 0.2f) { delta = 0.2f; }
            hpBar.fillAmount = Mathf.MoveTowards (hpBar.fillAmount, playerScript.normalizedHP, 2f * delta * Time.deltaTime);
            hpBar.color = barColors.Evaluate (hpBar.fillAmount);
        }
        if (!isLoading && Input.GetKeyDown (KeyCode.T)) {
            isLoading = true;
            StartCoroutine (RestartProcess ());
        }
	}

    IEnumerator RestartProcess () {
        restartPanel.gameObject.SetActive (true);
        while (restartPanel.color.a != 1) {
            restartPanel.color = FadeColorAlpha (restartPanel.color, 1, restartSpeed * Time.deltaTime);
            yield return null;
        }

        playerScript = null;
        AsyncOperation operation = SceneManager.LoadSceneAsync (SceneManager.GetActiveScene ().buildIndex);

        while (!operation.isDone) {
            yield return null;
        }

        playerScript = FindPlayerInstance ();

        while (restartPanel.color.a != 0) {
            restartPanel.color = FadeColorAlpha (restartPanel.color, 0, restartSpeed * Time.deltaTime);
            yield return null;
        }
        isLoading = false;
        yield return null;
    }

    Color FadeColorAlpha (Color currentColor, float targetAlpha, float fadePace) {
        return new Color (currentColor.r, currentColor.g, currentColor.b, Mathf.MoveTowards (currentColor.a, targetAlpha, fadePace));
    }

    PlayerScript FindPlayerInstance () {
        return GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerScript> ();
    }
}
