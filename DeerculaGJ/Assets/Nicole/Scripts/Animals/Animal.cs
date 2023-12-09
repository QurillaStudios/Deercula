using System.Collections;
using System.Collections.Generic;
using UnityEditor.XR;
using UnityEngine;
using UnityEngine.AI;
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
    protected NavMeshAgent agent;

    protected Vector2 currentDestination;

    protected GameObject player;

    private Vector2 lastFramePosition;
    protected bool lookRight = true;
    [SerializeField] protected float sightRange;
    [SerializeField] protected List<Transform> flightPoints;
    [SerializeField] protected AudioSource deadSound;

    protected virtual void Start()
    {
        lastFramePosition= transform.position;
        rb = GetComponent<Rigidbody2D>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed= speed;
        currentTimer = timer;
        startPosition = transform.position;

        player = GameObject.FindGameObjectWithTag("Deercula");
    }

    protected virtual void Update()
    {
        GetCurrentDirection();
    }

    protected virtual void FixedUpdate()
    {
        //Movement();
        RandomMovement();
    }

    public virtual void TakeDamage()
    {
        if (isBitable)
        {
            deadSound.Play();
            health--;
            Debug.Log("Health" + name +":" + health);
        }
        //else
        //{
        //}
        //    StartCoroutine(Fleeing());
    }

    //IEnumerator Fleeing()
    //{
    //    isFleeing = true;
    //    Debug.Log("isFleeing:" + isFleeing);
    //    yield return new WaitForSeconds(5f);
    //    isFleeing = false;
    //    Debug.Log("isFleeing:" + isFleeing);
    //}

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

    protected virtual void RandomMovement()
    {
        currentTimer -= Time.deltaTime;

        if(Vector2.Distance(player.transform.position,transform.position) < sightRange && !isFleeing)
        {
            currentDestination = flightPoints[Random.Range(0, flightPoints.Count)].position;
            isFleeing = true;
        }
        else if(isFleeing && Vector2.Distance(currentDestination, transform.position) < 0.6f)
        {
            isFleeing = false;
        }
        else if(!isFleeing)
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
        agent.speed= newSpeed;
    }

    private void GetCurrentDirection()
    {
        if(transform.position.x < lastFramePosition.x)
        {
            lookRight = false;
        }
        else if(transform.position.x > lastFramePosition.x)
        {
            lookRight= true;
        }

        lastFramePosition = transform.position;
    }
}
