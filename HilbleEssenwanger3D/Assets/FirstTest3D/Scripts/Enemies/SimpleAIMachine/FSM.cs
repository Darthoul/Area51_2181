using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM {

    int[,] stateMatrix;
    int stateIndex;
    public int currentStateIndex { get { return stateIndex; } }

    public FSM (int stateCount, int eventCount) {
        stateMatrix = new int[stateCount, eventCount];
    }

    public void SetRelation (int stateValue, int eventValue, int relationValue = -1) {
        stateMatrix[stateValue, eventValue] = relationValue;
    }

    static public FSM Create (int stateCount, int eventCount) {
        FSM fsm = new FSM (stateCount, eventCount);
        for (int j = 0; j < eventCount; j++) {
            for (int i = 0; i < stateCount; i++) {
                fsm.SetRelation (i, j);
            }
        }
        return fsm;
    }

    public void SendEvent (int eventIndexValue) {
        int nextState = stateMatrix[stateIndex, eventIndexValue];
        stateIndex = nextState != -1 ? nextState : stateIndex;
    }
}
