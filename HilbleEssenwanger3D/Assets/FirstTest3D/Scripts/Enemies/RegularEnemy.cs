using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularEnemy : EnemyEntity {

    void Update () {
        SetRenderColor (colorIndex);
    }
}
