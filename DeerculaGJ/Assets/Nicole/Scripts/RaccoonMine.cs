using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaccoonMine : MonoBehaviour
{
    [SerializeField] private float deadTimer;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, deadTimer);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Deercula")
        {
            collision.gameObject.GetComponent<Deercula>().TakeDamage();
        }
    }
}
