using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : Animal
{
    public override void TakeDamage()
    {
        base.TakeDamage();
        if (health <= 0)
        {
            GameManager.instance.Rabbits.Remove(this);
            Destroy(gameObject);
        }
    }

    //Bewegung: normal, läuft im zickzack vor spieler weg
    //angreifbar: nach eichhörnchen
    //greift an: nein
}
