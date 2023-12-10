using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Rabbit : Animal
{
    [SerializeField] private bool isHide;
    [SerializeField] private float hidingTime;
    [SerializeField] private float timeToHide;
    [SerializeField] private SpriteRenderer spriteRenderer;


    public override void TakeDamage()
    {
        base.TakeDamage();
        if (health <= 0)
        {
            GameManager.instance.Rabbits.Remove(this);
            Destroy(gameObject);
        }
    }

    protected override void Update()
    {
        if (!isHide)
        {
            base.Update();
        }
    }

    protected override void FixedUpdate()
    {
        if (!isHide)
        {
            base.FixedUpdate();
        }
    }

    private IEnumerator Hide()
    {
        isHide = true;
        GetComponent<Animator>().SetTrigger("TryToHide");
        yield return new WaitForSeconds(timeToHide);
        //Debug.Log("HasenHide" + isHide);
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(hidingTime);
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Collider2D>().enabled = true;
        isHide = false;
        isFleeing= false;
    }

    protected override void RandomMovement()
    {
        currentTimer -= Time.deltaTime;
        float newSpeed = speed;

        if (Vector2.Distance(player.transform.position, transform.position) < sightRange && !isFleeing)
        {
            currentDestination = transform.position;
            newSpeed = 0;
            isFleeing = true;
            StartCoroutine(Hide());

            //Debug.Log("HaseFlucht" + isFleeing);
        }
        else if (!isFleeing)
        {
            if (Vector2.Distance(currentDestination, transform.position) < 0.6f || currentTimer <= 0)
            {
                currentDestination = new Vector2(startPosition.x + Random.Range(-5f, 6f), startPosition.y + Random.Range(-5f, 6f));
                currentTimer = timer;
            }
        }

        agent.SetDestination(currentDestination);

        if (isFleeing)
        {
            newSpeed = speed * speedMultiplier;
        }
        else
        {
            newSpeed = speed;
        }
        agent.speed = newSpeed;
        spriteRenderer.flipX = !lookRight;
    }

    private void OnDestroy()
    {
        Instantiate(bloodPrefab, transform.position, Quaternion.identity);
    }
}
