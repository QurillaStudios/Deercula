using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raccoon : Animal
{
    public override void TakeDamage()
    {
        base.TakeDamage();
        if (health <= 0)
        {
            GameManager.instance.Raccoons.Remove(this);
            Destroy(gameObject);
        }
    }

    //Bewegung: normal, läuft langsam vor spieler weg
    //angreifbar: ja, aber nur von hinten
    //greift an: nein

}
