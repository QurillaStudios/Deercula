using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : Animal
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private GameObject bloodPrefab;

    private bool isAttacking = false;

    [SerializeField] private float frequenz;
    private float currentFrequenz;
    [SerializeField] private GameObject attackRangeEffect;

    [SerializeField] private GameObject pawPrefab;

    [SerializeField] private AudioSource phaseChangeSound;
    [SerializeField] private AudioSource attackSound;

    private bool isPhaseA = true;
    private bool isPhaseB = false;
    private bool isPhaseC = false;
    private bool isPhaseD = false;

    private bool standAttackPhaseWasTriggered = false;

    private bool isWaitingForEnd;

    protected override void Start()
    {
        base.Start();
        currentFrequenz = frequenz;
    }

    protected override void Update()
    {
        base.Update();
        if (isPhaseA)
        {
            BearWalkAttack();
        }
        else if (isPhaseB)
        {
            if(standAttackPhaseWasTriggered == false)
            {
                standAttackPhaseWasTriggered=true;
                StartCoroutine(BearStandAttack());
            }
            
        }
        else if (isPhaseC)
        {
            standAttackPhaseWasTriggered = false;
            BearWalkAttack();
        }
        else if (isPhaseD)
        {
            if (standAttackPhaseWasTriggered == false)
            {
                standAttackPhaseWasTriggered = true;
                StartCoroutine(BearStandAttack());
            }
        }


        //if (Vector2.Distance(transform.position, player.transform.position) < 2f)
        //{
        //    animator.SetTrigger("StartAttack");
        //    isAttacking = true;
        //}
    }
    public override void TakeDamage()
    {
        Debug.Log("BEAR ATTACKED");
        //base.TakeDamage();
        //if (health <= 0)
        //{
        //    GameManager.instance.IsGameOver = false;
        //    GameManager.instance.IsGameRunning = false;
        //    Destroy(gameObject);
        //}
        deadSound.Play();

        if (isPhaseA)
        {
            StartCoroutine(StartPhaseB());
            phaseChangeSound.Play();
        }
        if (isPhaseB)
        {
            StartCoroutine(StartPhaseC());
            phaseChangeSound.Play();
        }
        if (isPhaseC)
        {
            StartCoroutine(StartPhaseD());
            phaseChangeSound.Play();
        }
        if (isPhaseD)
        {
            if(!isWaitingForEnd)
            {
                StartCoroutine(GameEnd());
            }
        }

    }

    private IEnumerator GameEnd()
    {
        isWaitingForEnd = true;
        GameObject blood = Instantiate(bloodPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
        yield return new WaitForSeconds(0.75f);
        GameManager.instance.IsGameOver = false;
        GameManager.instance.IsGameRunning = false;
        //blood.transform.localScale = new Vector3(10f, 10f, 10f);
    }

    private IEnumerator StartPhaseB()
    {
        isPhaseA = false;

        animator.SetTrigger("StartAttack");
        agent.isStopped = true;
        yield return new WaitForSeconds(1f);
        isPhaseB = true;
    }

    private IEnumerator StartPhaseC()
    {
        isPhaseB = false;
        animator.SetTrigger("StopAttack");

        yield return new WaitForSeconds(1f);
        agent.isStopped = false;
        isPhaseC = true;
    }

    private IEnumerator StartPhaseD()
    {
        isPhaseC = false;

        animator.SetTrigger("StartAttack");
        agent.isStopped = true;
        yield return new WaitForSeconds(1f);
        isPhaseD = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.tag == "Deercula")
        //{
        //    collision.gameObject.GetComponent<Deercula>().TakeDamage();
        //}
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
        if (!isAttacking)
        {
            agent.SetDestination(player.transform.position);
            spriteRenderer.flipX = !lookRight;
        }
        //else
        //{ base.RandomMovement(); }


    }


    private void BearWalkAttack()
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


    private IEnumerator BearStandAttack()
    {
        while (isPhaseB || isPhaseD)
        {
            Vector2 playerPosition = player.transform.position;
            GameObject goIndicator = Instantiate(pawPrefab, transform.position, Quaternion.identity);
            goIndicator.GetComponent<Paw>().SetPawInactive(playerPosition);
            yield return new WaitForSeconds(0.4f);
            GameObject go = Instantiate(pawPrefab, transform.position, Quaternion.identity);
            attackSound.Play();
            go.GetComponent<Paw>().SetPawActive(playerPosition);
            yield return new WaitForSeconds(1.2f);
        }
    }


    IEnumerator Attacking()
    {
        isAttacking = true;
        attackSound.Play();
        attackRangeEffect.SetActive(true);
        yield return new WaitForSeconds(2f);
        attackRangeEffect.SetActive(false);
        isAttacking = false;
        currentFrequenz = frequenz;
    }

    //Bewegung: bis zur Aktivierung nein, danach ja
    //Angreifbar: nach Wölfen
    //greift an: ja
}
