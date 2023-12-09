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
            GameManager.instance.BabyDeers.Remove(this);
            Destroy(gameObject);
        }
    }

    protected override void RandomMovement()
    {
        agent.SetDestination(player.transform.position);
        spriteRenderer.flipX = !lookRight;
    }
}
