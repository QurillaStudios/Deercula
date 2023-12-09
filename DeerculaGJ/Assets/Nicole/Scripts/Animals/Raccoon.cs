using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Raccoon : Animal
{
    [SerializeField] private float frequenz;
    private float currentFrequenz;
    [SerializeField] private GameObject mine;
    [SerializeField] private SpriteRenderer spriteRenderer;

    protected override void Start()
    {
        base.Start();
        currentFrequenz = frequenz;
    }

    public override void TakeDamage()
    {
        base.TakeDamage();
        if (health <= 0)
        {
            GameManager.instance.Raccoons.Remove(this);
            Destroy(gameObject);
        }
    }

    protected override void Update()
    {
        base.Update();
        RandomMine();
    }

    private void RandomMine()
    {
        currentFrequenz -= Time.deltaTime;

        if(currentFrequenz <= 0)
        {
            Instantiate(mine, gameObject.transform.position, Quaternion.identity );
            currentFrequenz = frequenz;
        }

    }

    protected override void RandomMovement()
    {
        currentTimer -= Time.deltaTime;
       
        if (Vector2.Distance(currentDestination, transform.position) < 0.6f || currentTimer <= 0)
        {
            currentDestination = new Vector2(startPosition.x + Random.Range(-5f, 6f), startPosition.y + Random.Range(-5f, 6f));
            currentTimer = timer;
        }
        
        agent.SetDestination(currentDestination);

        float newSpeed = speed;
        if (isFleeing)
        {
            newSpeed = speed * speedMultiplier;
        }

        agent.speed = newSpeed;

        spriteRenderer.flipX = lookRight;
    }
}
