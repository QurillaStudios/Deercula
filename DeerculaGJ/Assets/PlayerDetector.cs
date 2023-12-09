using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    [SerializeField] private GameObject vignette;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Deercula")
        {
            if (!GetComponentInParent<Wolf>().IsBitable)
                vignette.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Deercula")
        {
                vignette.SetActive(false);
        }
    }
}
