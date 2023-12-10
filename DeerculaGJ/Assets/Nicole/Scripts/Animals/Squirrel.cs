using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squirrel : Animal
{
    [SerializeField] private bool isHide;
    [SerializeField] private float hidingTime;
    [SerializeField] private SpriteRenderer spriteRenderer;

    public override void TakeDamage()
    {
        base.TakeDamage();
        if (health <= 0)
        {
            GameManager.instance.Squirrels.Remove(this);
            Destroy(gameObject);
        }
    }

    protected override void FixedUpdate()
    {
        if (isFleeing)
        {
                spriteRenderer.flipX = !lookRight;
            //Debug.Log("will sich verstecken");
            if (Vector2.Distance(currentDestination, transform.position) < 0.6f)
            {
                //Debug.Log("ist dabei sich zu verstecken");
                StartCoroutine(Hide());
                isFleeing = false;
                //Debug.Log("Flucht2" + isFleeing);
            }
        }
        else if (!isHide)
        {
            base.FixedUpdate();
        }
    }

    private IEnumerator Hide()
    {
        isHide = true;
        //Debug.Log("Hide" + isHide);
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(hidingTime);
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Collider2D>().enabled = true;
        isHide = false;
    }

    protected override void RandomMovement()
    {
        currentTimer -= Time.deltaTime;

        if (Vector2.Distance(player.transform.position, transform.position) < sightRange && !isFleeing)
        {
            currentDestination = flightPoints[Random.Range(0, flightPoints.Count)].position;
            isFleeing = true;
            //Debug.Log("Flucht" + isFleeing);
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

        float newSpeed = speed;
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

    //Bewegung: normal, läuft vor spieler weg, flüchtet auf Baum, spawnt einige Zeit später wieder
    //angreifbar: ja, nach Maus
    //greift an: nein

    private void OnDestroy()
    {
        Instantiate(bloodPrefab, transform.position, Quaternion.identity);
    }
}
