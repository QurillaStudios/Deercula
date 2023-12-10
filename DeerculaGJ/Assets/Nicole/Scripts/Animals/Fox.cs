using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : Animal
{
    [SerializeField] private float frequenz;
    private float currentFrequenz;
    private bool isAttacking;
    [SerializeField] private GameObject attackRangeEffect;
    [SerializeField] private SpriteRenderer spriteRenderer;
    bool hasHited;

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
            GameManager.instance.Foxes.Remove(this);
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
        if (!isAttacking)
        {
            currentFrequenz -= Time.deltaTime;
        }

        if (currentFrequenz < 0)
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
        isAttacking = false;
        currentFrequenz = frequenz;
    }

    IEnumerator Hit()
    {
        yield return new WaitForSeconds(1f);
        hasHited= false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Deercula")
        {
            if (isAttacking && !hasHited)
            {
                hasHited = true;
                collision.gameObject.GetComponent<Deercula>().TakeDamage();
                StartCoroutine(Hit());
            }   
        }
    }

    protected override void RandomMovement()
    {
        base.RandomMovement(); 
        spriteRenderer.flipX = lookRight;
    }

    private void OnDestroy()
    {
        Instantiate(bloodPrefab, transform.position, Quaternion.identity);
    }
}
