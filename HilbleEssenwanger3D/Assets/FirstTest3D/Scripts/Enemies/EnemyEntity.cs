using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : DamageableObject {

    public int health;
    protected bool invulnerable = false;
    public float colorIndex = 0f;
    public Gradient damageGradient;
    public Renderer enemyRenderer;

    public FSM enemyStateMachine;

    public delegate void EnemyBehaviour ();
    public event EnemyBehaviour currentBehaviour;

    protected virtual void Start () {
        enemyRenderer = transform.GetChild (1).GetComponent<Renderer> ();
    }

    protected virtual void Update () {
        Debug.Log ("Updating parent");
        if (currentBehaviour != null) {
            currentBehaviour ();
        }
    }

    protected void SetRenderColor (float gradientPick) {
        for (int i = 0; i < enemyRenderer.materials.Length; i++) {
            enemyRenderer.materials[i].color = damageGradient.Evaluate (gradientPick);
        }
    }
    protected void SetCurrentBehaviour (EnemyBehaviour enemyBehaviour) {
        currentBehaviour = enemyBehaviour;
    }
    protected virtual void SendEnemyEvent (int eventIndex) {
        enemyStateMachine.SendEvent (eventIndex);
    }

    public override void TakeDamage () {
        if (!invulnerable) {
            Debug.Log ("TakeDamage!");
            health--;
            GetComponent<Animator> ().SetTrigger ("TakeDamage");
            invulnerable = true;
        }
    }

    public void ResetInvulnerable () {
        invulnerable = false;
        if (health <= 0) {
            QuestManager.instance.Check ("destroy", name);
            Destroy (gameObject);
        }
    }

    //OnTriggerEvent Unity Functions
    public virtual void TriggerEnterCall (GameObject objRef) {
        //EMPTY (Call on children only)
    }
    public virtual void TriggerExitCall (GameObject objRef) {
        //EMPTY (Call on children only)
    }
}
