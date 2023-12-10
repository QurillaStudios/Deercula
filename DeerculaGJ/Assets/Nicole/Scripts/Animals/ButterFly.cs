using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterFly : Animal
{
    public SpriteRenderer spriteRenderer;

    public override void TakeDamage()
    {
        base.TakeDamage();
        if (health <= 0)
        {
            GameManager.instance.ButterFlies.Remove(this);
            Destroy(gameObject);
        }
    }

    protected override void RandomMovement()
    {
        base.RandomMovement();
        spriteRenderer.flipX = lookRight;
    }

    private void OnDestroy()
    {
        Instantiate(bloodPrefab, transform.position, Quaternion.identity);
    }
}
