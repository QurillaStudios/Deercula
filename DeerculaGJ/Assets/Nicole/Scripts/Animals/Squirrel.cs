using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squirrel : Animal
{
    public override void TakeDamage()
    {
        base.TakeDamage();
        if (health <= 0)
        {
            GameManager.instance.Squirrels.Remove(this);
            Destroy(gameObject);
        }
    }

    //Bewegung: normal, l�uft vor spieler weg, fl�chtet auf Baum, spawnt einige Zeit sp�ter wieder
    //angreifbar: ja, nach Maus
    //greift an: nein
}
