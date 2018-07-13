using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour {

    static public QuestManager instance;
    public QuestActions questActions;

    public List<Quest> inactive;
    public List<Quest> active;
    public List<Quest> completed;

    void Awake () {
        if (instance == null) {
            instance = this;
        }
    }

    // Use this for initialization
    void Start () {
        //Initialized all the Quest Lists
        inactive = new List<Quest> ();
        active = new List<Quest> ();
        completed = new List<Quest> ();
        //Created some Quests
        inactive.Add (new Quest ("QT01", "obtain", "FireBall", 1, "QT02").SetMessage("DisableGate", 0));
        inactive.Add (new Quest ("QT02", "destroy", "BasicEnemy", 1).SetMessage("DisableGate", 1));
        //Added my first Quest to active
        Activate ("QT01");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Check (string action, string type) {
        List<Quest> removal = new List<Quest> ();
        for (int i = 0; i < active.Count; i++) {
            if (active[i].Check (action, type)) {
                Debug.Log ("Completed " + active[i].id);
                questActions.SendMessage (active[i].message.methodName, active[i].message.paramValue);
                removal.Add (active[i]);
                i--;
            }
        }
        foreach (Quest quest in removal) {
            active.Remove (quest);
            completed.Add (quest);
        }
        removal.Clear ();
    }

    public void Activate(string id) {
        Quest quest = Search (id);
        inactive.Remove (quest);
        active.Add (quest);
    }

    public Quest Search (string id) {
        return inactive.Find (quest => quest.id == id);
    }
}
