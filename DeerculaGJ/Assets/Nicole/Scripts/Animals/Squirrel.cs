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

    //Bewegung: normal, läuft vor spieler weg, flüchtet auf Baum, spawnt einige Zeit später wieder
    //angreifbar: ja, nach Maus
    //greift an: nein
}
