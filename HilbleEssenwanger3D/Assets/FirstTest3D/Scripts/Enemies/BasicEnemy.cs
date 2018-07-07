using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : EnemyObject {

    public int health;
    bool invulnerable = false;
    public PlatformMovement3D target;
    public Vector3 planarTargetDistance { get { return new Vector3 (target.transform.position.x, transform.position.y, target.transform.position.z); }}
    public float colorIndex = 0f;
    public Gradient damageGradient;
    public Renderer enemyRenderer;

    void Start () {
        enemyRenderer = transform.GetChild (1).GetComponent<Renderer> ();
    }

    void Update () {
        if (target != null) {
            transform.forward = (planarTargetDistance - transform.position).normalized;
        }
        for (int i = 0; i < enemyRenderer.materials.Length; i++) {
            enemyRenderer.materials[i].color = damageGradient.Evaluate (colorIndex);
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
            Destroy (gameObject);
        }
    }
}
