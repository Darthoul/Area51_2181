using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect {

    protected Color color;
    protected string name;
    public string componentName { get { return name; }}

    protected Effect () {
        
    }

    static public Effect CreateEmpty () {
        Effect effect = new Effect ();
        effect.name = "None";
        return effect;
    }

    public Effect Apply (EnemyEntity enemyEntity) {
        if (name == "None") { return null; }
        Effect effect;
        OnApply (ref enemyEntity, out effect);
        return effect;
    }

    public virtual bool Update (float frameDelta) {
        return true;
    }

    protected virtual void OnApply (ref EnemyEntity enemyEntity, out Effect effect) {
        effect = CreateEmpty ();
    }
}
