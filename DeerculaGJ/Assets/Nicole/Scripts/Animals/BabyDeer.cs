using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyDeer : Animal
{
    public override void TakeDamage()
    {
        base.TakeDamage();
        if (health <= 0)
        {
            GameManager.instance.BabyDeers.Remove(this);
            Destroy(gameObject);
        }
    }
}
