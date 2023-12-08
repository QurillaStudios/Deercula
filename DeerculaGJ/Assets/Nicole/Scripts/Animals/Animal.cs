using System.Collections;
using System.Collections.Generic;
using UnityEditor.XR;
using UnityEngine;
using UnityEngine.UIElements;

public class Animal : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private float speed;
    [SerializeField] private float speedMultiplier; 
    [SerializeField] private float movementRange;
    [SerializeField] private bool isBitable = false;

    public bool IsBitable { get => isBitable; set => isBitable = value; }
    private bool isFleeing;

    private Vector2 startPosition;
    private Vector2 moveDirection;

    [SerializeField] private float timer = 3f;
    private float currentTimer;
    private Rigidbody2D rb;

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
        }
        else
        {
            StartCoroutine(Fleeing());
        }
    }

    IEnumerator Fleeing()
    {
        isFleeing = true;
        yield return new WaitForSeconds(5f);
        isFleeing = false;
    }

    protected virtual void Movement()
    {
        currentTimer -= Time.deltaTime;
        Debug.Log("Timer" + currentTimer); 

        if (currentTimer <= 0)
        {
            moveDirection = new Vector2(Random.Range(-1, 2), Random.Range(-1, 2));
            Debug.Log("New Timer Direction" + moveDirection);
            currentTimer = timer;
        }

        if(Vector2.Distance(startPosition, transform.position) >= movementRange)
        {
            moveDirection.Normalize();
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
