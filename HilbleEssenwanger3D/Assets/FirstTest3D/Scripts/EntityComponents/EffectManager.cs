using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour {

    static public EffectManager instance;
    List<Effect> effects;

    void Awake () {
        if (instance == null) {
            instance = this;
        }
    }

	// Use this for initialization
	void Start () {
        //Initialized the effect List
        effects = new List<Effect> ();
        //Created some Effects
        effects.Add (new OvertimeEffect ("Frost", new Color (0.36f, 0.75f, 1, 1f), 2f));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Effect Search (string targetName) {
        Effect retEffect = effects.Find (effect => effect.componentName == targetName);
        return (retEffect != null) ? retEffect : Effect.CreateEmpty();
    }
}
