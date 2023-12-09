using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : Animal
{
    public override void TakeDamage()
    {
        base.TakeDamage();
        if (health <= 0)
        {
            GameManager.instance.Mouses.Remove(this);
            Destroy(gameObject);
        }
    }
        
}