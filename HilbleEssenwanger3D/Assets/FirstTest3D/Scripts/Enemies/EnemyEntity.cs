using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : DamageableObject {

    public int health;
    protected bool invulnerable = false;
    public float colorIndex = 0f;
    public Gradient damageGradient;
    public Renderer enemyRenderer;
    public Color currentBase;
    protected Color baseColor;
    public bool changedBaseColor { get { return currentBase != baseColor; } }

    public float speed = 4.5f;

    public FSM enemyStateMachine;

    public delegate void EnemyBehaviour ();
    public event EnemyBehaviour currentBehaviour;

    protected Effect effect;

    protected virtual void Start () {
        enemyRenderer = transform.GetChild (1).GetComponent<Renderer> ();
        currentBase = baseColor = enemyRenderer.material.color;
    }

    protected virtual void Update () {
        if (currentBehaviour != null) {
            currentBehaviour ();
        }
        if (effect != null) {
            if (effect.Update (Time.deltaTime)) {
                currentBase = baseColor;
                speed = 4.5f;
                effect = null;
            }
        }
    }

    protected void SetRenderColor (float gradientPick) {
        if (changedBaseColor || gradientPick != 0f) {
            Color targetColor = damageGradient.Evaluate (gradientPick);
            if (changedBaseColor) { targetColor = (targetColor * 0.7f) + (currentBase * 0.3f); }
            for (int i = 0; i < enemyRenderer.materials.Length; i++) {
                enemyRenderer.materials[i].color = targetColor;
            }
        }
    }
    protected void SetCurrentBehaviour (EnemyBehaviour enemyBehaviour) {
        currentBehaviour = enemyBehaviour;
    }
    protected virtual void SendEnemyEvent (int eventIndex) {
        enemyStateMachine.SendEvent (eventIndex);
    }

    public override void TakeDamage (string effectName = null) {
        if (!invulnerable) {
            Debug.Log ("TakeDamage!");
            health--;
            GetComponent<Animator> ().SetTrigger ("TakeDamage");
            invulnerable = true;

            if (effectName != null) {
                Debug.Log ("Will try to apply " + effectName);
                effect = EffectManager.instance.Search (effectName).Apply (this);
            }
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
