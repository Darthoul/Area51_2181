using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : EnemyEntity {

    public PlatformMovement3D target;
    public Vector3 planarTargetDistance { get { return new Vector3 (target.transform.position.x, transform.position.y, target.transform.position.z); }}

    protected override void Update () {
        base.Update ();
        if (target != null) {
            transform.forward = (planarTargetDistance - transform.position).normalized;
        }
        SetRenderColor (colorIndex);
    }
}
