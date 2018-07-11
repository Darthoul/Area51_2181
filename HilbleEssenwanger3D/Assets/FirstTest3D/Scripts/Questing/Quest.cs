using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest {

    public readonly string id;
    public string action;
    public string type;
    public int currentAmmount;
    public int targetAmmount;

    public string next;

    public Quest (string id, string action, string type, int targetAmmount, string next = null) {
        this.id = id;
        this.action = action;
        this.type = type;
        this.targetAmmount = targetAmmount;
        this.next = next;
        currentAmmount = 0;
    }

    public bool Check (string action, string type, int ammount = 1) {
        if (action == this.action && type == this.type) {
            currentAmmount += ammount;
            if (currentAmmount == targetAmmount) {
                if (!string.IsNullOrEmpty(next)) {
                    QuestManager.instance.Activate (next);
                }
                return true;
            }
        }
        return false;
    }
}
