using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class RegularEnemy : EnemyEntity {

    public enum RegularState {
        Patrol,
        Follow
    }

    Transform followTarget;
    float resetFollowTime = 3f;
    float currentFollowTime = 0f;
    bool insideFollowReach = false;
    Vector3 planarTargetDistance { get { return new Vector3 (followTarget.transform.position.x, transform.position.y, followTarget.transform.position.z); } }

    protected override void Start () {
        base.Start ();
        enemyStateMachine = FSM.Create (2, 2);
        enemyStateMachine.SetRelation (0, 0, 1);
        enemyStateMachine.SetRelation (1, 1, 0);

        SetCurrentBehaviour (GetBehaviourByIndexName (enemyStateMachine.currentStateIndex));
    }

    protected override void Update () {
        base.Update ();
        SetRenderColor (colorIndex);
    }

    protected override void SendEnemyEvent (int eventIndex) {
        base.SendEnemyEvent (eventIndex);
        SetCurrentBehaviour (GetBehaviourByIndexName (enemyStateMachine.currentStateIndex));
    }

    public EnemyBehaviour GetBehaviourByIndexName (int targetIndex) {
        MethodInfo methodInfo = GetType().GetMethod(((RegularState) targetIndex).ToString(), BindingFlags.Instance | BindingFlags.NonPublic);
        Debug.Log ("__" + methodInfo.Name);
        return (EnemyBehaviour) System.Delegate.CreateDelegate (typeof (EnemyBehaviour), this, methodInfo);
    }

    void Patrol () {
        Debug.Log ("Im on Patrol");
        transform.Rotate (Vector3.up * 85f * Time.deltaTime);
    }

    void Follow () {
        Debug.Log ("Following a target");
        transform.forward = (planarTargetDistance - transform.position).normalized;
        transform.position += transform.forward * 4.5f * Time.deltaTime;
        if (!insideFollowReach) {
            currentFollowTime += Time.deltaTime;
        }
        if (currentFollowTime >= resetFollowTime) {
            currentFollowTime = 0f;
            insideFollowReach = false;
            followTarget = null;
            SendEnemyEvent (1);
        }
    }

    public override void TriggerEnterCall (GameObject objRef) {
        followTarget = objRef.transform;
        insideFollowReach = true;
        currentFollowTime = 0f;
        SendEnemyEvent (0);
    }

    public override void TriggerExitCall (GameObject objRef) {
        insideFollowReach = false;
    }
}
