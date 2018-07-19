using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : DamageableObject {

    public int health;
    protected bool invulnerable = false;
    public float colorIndex = 0f;
    public Gradient damageGradient;
    public Renderer enemyRenderer;

    void Start () {
        enemyRenderer = transform.GetChild (1).GetComponent<Renderer> ();
    }

    protected void SetRenderColor (float gradientPick) {
        for (int i = 0; i < enemyRenderer.materials.Length; i++) {
            enemyRenderer.materials[i].color = damageGradient.Evaluate (gradientPick);
        }
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
}
