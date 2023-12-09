using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterFly : Animal
{
    public override void TakeDamage()
    {
        base.TakeDamage();
        if (health <= 0)
        {
            GameManager.instance.ButterFlies.Remove(this);
            Destroy(gameObject);
        }
    }
}
