using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deercula : MonoBehaviour
{
    [SerializeField]
    private int initialHealth;
    private int health;
    Rigidbody2D body;

    float horizontal;
    float vertical;

    public float runSpeed = 20.0f;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        health = initialHealth;
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
    }

    public void TakeDamage()
    {
        initialHealth -= 1;
        Debug.Log("Damage taken");
    }
}
