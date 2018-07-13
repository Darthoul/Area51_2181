using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest {

    public class Message {
        public string methodName;
        public object paramValue;

        public Message (string methodName, object paramValue) {
            this.methodName = methodName;
            this.paramValue = paramValue;
        }
    }

    public readonly string id;
    public string action;
    public string type;
    public int currentAmmount;
    public int targetAmmount;

    public Message message;

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

    public Quest SetMessage (string method, object param) {
        message = new Message (method, param);
        return this;
    }
}
