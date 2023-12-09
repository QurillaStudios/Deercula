using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyTrap : MonoBehaviour
{
    [SerializeField] private Sprite sprite;
    private bool isAttackStarted;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Deercula")
        {
            if (!isAttackStarted)
            {
                isAttackStarted = true;
                StartCoroutine(AttackAnimation(collision));
                collision.gameObject.GetComponent<Deercula>().TakeDamage();
            }
        }
    }

    IEnumerator AttackAnimation(Collider2D collision)
    {
        transform.up = collision.gameObject.transform.position - transform.position;
        GetComponent<Animator>().SetTrigger("Snap");
        yield return new WaitForSeconds(1f);
        //GetComponent<Animator>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = sprite;
        transform.rotation = new Quaternion(0,0,0,0);
        isAttackStarted= false;
    }

    //Bewegung: keine
    //Angreifbar: nein
    //greift an: ja
}