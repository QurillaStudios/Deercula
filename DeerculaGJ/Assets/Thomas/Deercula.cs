using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Deercula : MonoBehaviour
{
    [SerializeField]
    private int initialHealth;
    [SerializeField] private float attackCoolDownTime = 1f;
    [SerializeField]
    private LayerMask bitableLayerMask;
    [SerializeField]
    private GameObject BiteIndicator;

    private float currentAttackCoolDownTimer = 0f;
    private bool canAttack = true;

    private int health;
    Rigidbody2D body;

    float horizontal;
    float vertical;

    public float runSpeed = 20.0f;

    private Vector3 lastMovementDirection;
    [SerializeField] private AudioSource walking;
    [SerializeField] private AudioSource attack;
    [SerializeField] private AudioSource takedamage;



    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        health = initialHealth;
        BiteIndicator.SetActive(false);
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if(Input.GetKeyDown(KeyCode.Space))
        {
            OnAttackTriggered();
        }

        if(new Vector2(horizontal, vertical) != Vector2.zero)
        {
            walking.Play();
            lastMovementDirection = new Vector2(horizontal, vertical);
            if( horizontal != 0f && vertical != 0f )
            {
                lastMovementDirection = lastMovementDirection / 1.5f;
            }
        }
        else
        {
            walking.Stop();
        }

        //Debug.Log("LastMovementDirection: " + lastMovementDirection);
    }

    private void FixedUpdate()
    {
        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
    }



    private void OnAttackTriggered()
    {
        if(canAttack)
        {
        Debug.Log("Attack triggered");
            Attack();
            StartCoroutine(AttackCooldown());
        }
    }

    private void Attack()
    {
        Vector2 attackCirclePosition = transform.position + lastMovementDirection;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackCirclePosition, 1f, bitableLayerMask);

        foreach(Collider2D collider in colliders)
        {
            if( collider.gameObject.tag == "Bitable")
            {
                attack.Play();
                collider.gameObject.GetComponent<Animal>().TakeDamage();
            }
        }

        StartCoroutine(ShowBiteIndicator(attackCirclePosition));
    }

    private IEnumerator AttackCooldown()
    {
        canAttack= false;
        yield return new WaitForSeconds(attackCoolDownTime);
        canAttack= true;
    }

    private IEnumerator ShowBiteIndicator(Vector2 position)
    {
        BiteIndicator.transform.position = position;
        BiteIndicator.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        BiteIndicator.SetActive(false);
    }

    public void TakeDamage()
    {
        takedamage.Play();
        health -= 1;
        Debug.Log("Damage taken");
        if (health <= 0)
        {
            GameManager.instance.IsGameOver = true;
            GameManager.instance.IsGameRunning = false;
            Debug.Log("GameOver");
        }
    }
}
