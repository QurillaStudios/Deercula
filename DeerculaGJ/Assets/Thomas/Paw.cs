using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paw : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    private Transform player;
    private bool canMove = false;
    private Vector2 destination;

    private Rigidbody2D rb;
    private CircleCollider2D myCollider;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<CircleCollider2D>();
        spriteRenderer= GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Deercula").transform;
        destination = player.position;
        canMove= true;
        myCollider.enabled = false;

        Destroy(gameObject, 0.7f);
    }

    void Update()
    {
        if(canMove)
        {
            float step = 20f * Time.deltaTime;

            // move sprite towards the target location
            transform.position = Vector2.MoveTowards(transform.position, destination, step);
        }
    }
    public void SetPawInactive(Vector2 playerPosition)
    {
        destination= playerPosition;
        myCollider.enabled= false;
    }

    public void SetPawActive(Vector2 playerPosition)
    {
        destination= playerPosition;
        myCollider.enabled = true;
        spriteRenderer.color = Color.red;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Deercula")
        {
            collision.gameObject.GetComponent<Deercula>().TakeDamage();
        }
    }
}
