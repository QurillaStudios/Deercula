using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : Animal
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField] private AudioSource wolfAttackSound;

    public override void TakeDamage()
    {
        base.TakeDamage();
        if (health <= 0)
        {
            GameManager.instance.Wolves.Remove(this);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Deercula")
        {
            if(!isBitable)
                wolfAttackSound.Play();
                collision.gameObject.GetComponent<Deercula>().TakeDamage();
        }
    }

    protected override void RandomMovement()
    {
        if(!isBitable)
        {
        agent.SetDestination(player.transform.position);

        }
        else
        { base.RandomMovement(); }

        spriteRenderer.flipX = !lookRight;
    }
}

