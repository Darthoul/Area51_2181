using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestActions : MonoBehaviour {

    public void DisableGate (int gateIndex) {
        GameObject.Find ("Gates").transform.GetChild (gateIndex).gameObject.SetActive (false);
    }
}
