using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    [SerializeField] private GameObject vignette;
    [SerializeField] private AudioSource wolfNearSound;
    [SerializeField] private Transform player;

    bool isActiv;

    private void Update()
    {
        if (Vector2.Distance(player.position, transform.position) < 10f)
        {
            if (!GetComponentInParent<Wolf>().IsBitable)
            {
                if(!isActiv)
                {
                    isActiv = true;
                    vignette.SetActive(true);
                    wolfNearSound.Play();
                }
            }
        }

        if(Vector2.Distance(player.position, transform.position) > 10f)
        {
            isActiv = false;
            vignette.SetActive(false);
        }
    }


  
    
}
