using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : Animal
{
    public override void TakeDamage()
    {
        base.TakeDamage();
        if (health <= 0)
        {
            GameManager.instance.Foxes.Remove(this);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Deercula")
        {
            collision.gameObject.GetComponent<Deercula>().TakeDamage();
        }
    }
}
