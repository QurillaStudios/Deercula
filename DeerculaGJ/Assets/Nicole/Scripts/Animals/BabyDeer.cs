using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyDeer : Animal
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;


    public override void TakeDamage()
    {
        base.TakeDamage();
        if (health <= 0)
        {
            deadSound.Play();
            GameManager.instance.BabyDeers.Remove(this);
            Destroy(gameObject);
        }
    }

    protected override void RandomMovement()
    {
        if(Vector2.Distance(player.transform.position, transform.position) > 5f)
        {
            agent.isStopped = false;
            GetComponent<Animator>().SetBool("walking", true);
            agent.SetDestination(player.transform.position);

        }
        else
        {
            GetComponent<Animator>().SetBool("walking", false);
            agent.isStopped = true;
        }
        spriteRenderer.flipX = !lookRight;
    }
}
