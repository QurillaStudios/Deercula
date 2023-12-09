using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Raccoon : Animal
{
    [SerializeField] private float frequenz;
    private float currentFrequenz;
    private bool isAttacking;
    [SerializeField] private GameObject attackRangeEffect;

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
        RandomAttack();
    }

    private void RandomAttack()
    {
        if(!isAttacking)
        {
            currentFrequenz -= Time.deltaTime;
        }

        if(currentFrequenz < 0 )
        {
            currentFrequenz = 0;
            StartCoroutine(Attacking());
        }
    }

    IEnumerator Attacking()
    {
        isAttacking = true;
        attackRangeEffect.SetActive(true);
        yield return new WaitForSeconds(2f);
        attackRangeEffect.SetActive(false);
        isAttacking= false;
        currentFrequenz = frequenz;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Deercula")
        {
            if (isAttacking)
                collision.gameObject.GetComponent<Deercula>().TakeDamage();
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
    }
   
    //Bewegung: normal, läuft langsam vor spieler weg
    //angreifbar: ja, aber nur von hinten
    //greift an: nein

}
