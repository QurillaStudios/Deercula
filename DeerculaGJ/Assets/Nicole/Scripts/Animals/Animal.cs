using System.Collections;
using System.Collections.Generic;
using UnityEditor.XR;
using UnityEngine;
using UnityEngine.UIElements;

public class Animal : MonoBehaviour
{
    [SerializeField] protected int health;
    [SerializeField] protected float speed;
    [SerializeField] protected float speedMultiplier; 
    [SerializeField] protected float movementRange;
    [SerializeField] protected bool isBitable = false;
    public bool IsBitable { get => isBitable; set => isBitable = value; }

    protected bool isFleeing;

    protected Vector2 startPosition;
    protected  Vector2 moveDirection;

    [SerializeField] protected float timer = 3f;
    protected float currentTimer;
    protected  Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentTimer = timer;
        startPosition = transform.position;
    }

    private void FixedUpdate()
    {
        Movement();
    }

    public virtual void TakeDamage()
    {
        if (isBitable)
        {
            health--;
            Debug.Log("Health" + name +":" + health);
        }
        else
        {
            StartCoroutine(Fleeing());
        }
    }

    IEnumerator Fleeing()
    {
        isFleeing = true;
        Debug.Log("isFleeing:" + isFleeing);
        yield return new WaitForSeconds(5f);
        isFleeing = false;
        Debug.Log("isFleeing:" + isFleeing);
    }

    protected virtual void Movement()
    {
        currentTimer -= Time.deltaTime;
        //Debug.Log("Timer" + currentTimer); 

        if (currentTimer <= 0)
        {
            moveDirection = new Vector2(Random.Range(-1, 2), Random.Range(-1, 2));
            Debug.Log("New Timer Direction" + moveDirection);
            currentTimer = timer;
        }

        if(Vector2.Distance(startPosition, transform.position) >= movementRange)
        {
            Vector2.Reflect(moveDirection, transform.position );
        }
       
        float newSpeed = speed;
        if(isFleeing)
        {
            newSpeed = speed * speedMultiplier;
        }

        Debug.Log("speed" + newSpeed);
        rb.velocity = moveDirection * newSpeed;
    }

}
