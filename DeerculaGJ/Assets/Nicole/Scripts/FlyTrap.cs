using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyTrap : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Deercula")
        {
            collision.gameObject.GetComponent<Deercula>().TakeDamage();
        }
    }
}