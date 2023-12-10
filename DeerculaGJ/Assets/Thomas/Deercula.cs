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

    public int health;
    Rigidbody2D body;

    float horizontal;
    float vertical;

    public float runSpeed = 20.0f;

    private Vector3 lastMovementDirection;
    [SerializeField] private AudioSource walking;
    [SerializeField] private AudioSource attack;
    [SerializeField] private AudioSource takedamage;

    private Animator deerAnimation;
    private SpriteRenderer deerRenderer;
    protected bool lookRight = true;
    private Vector2 lastFramePosition;

    private Vector3 bitIndicatorPosition;

    void Start()
    {
        deerAnimation = GetComponent<Animator>();
        deerRenderer = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
        health = initialHealth;
        BiteIndicator.SetActive(false);
        lastFramePosition= transform.position;
        bitIndicatorPosition = BiteIndicator.transform.localPosition;
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
            deerAnimation.SetBool("walking", true);
            Debug.Log(deerAnimation.GetBool("walking"));
            lastMovementDirection = new Vector2(horizontal, vertical);
            if( horizontal != 0f && vertical != 0f )
            {
                lastMovementDirection = lastMovementDirection / 1.5f;
            }
        }
        else
        {
            deerAnimation.SetBool("walking", false);
            Debug.Log(deerAnimation.GetBool("walking"));
            walking.Stop();
        }

        if (transform.position.x < lastFramePosition.x)
        {
            lookRight = false;
            bitIndicatorPosition.x = -1.99f;
        }
        else if (transform.position.x > lastFramePosition.x)
        {
            lookRight = true;
            bitIndicatorPosition.x = 1.99f;
        }

        BiteIndicator.transform.localPosition = bitIndicatorPosition;

        lastFramePosition = transform.position;
        deerRenderer.flipX = !lookRight;
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
            deerAnimation.SetTrigger("attacking");
            Debug.Log("Attack triggered");
            Attack();
            StartCoroutine(AttackCooldown());
        }
    }

    private void Attack()
    {
           
        Vector2 attackCirclePosition =BiteIndicator.transform.position + lastMovementDirection ;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackCirclePosition, 1.5f, bitableLayerMask);

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
