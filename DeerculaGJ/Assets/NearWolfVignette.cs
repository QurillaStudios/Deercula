using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class NearWolfVignette : MonoBehaviour
{
    private PostProcessVolume volume;
    [SerializeField] private float startBlur;
    [SerializeField] private float endBlur;
    private Vignette vignette;
 

    private void Start()
    {
        volume = GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings<Vignette>(out vignette);
 
    }
    // Update is called once per frame
    void Update()
    {

            vignette.smoothness.value = Mathf.Lerp(vignette.smoothness.value, endBlur, Time.deltaTime);
        
        //else if (currentBlur != startBlur)
        //{
        //    currentBlur = Mathf.Lerp(endBlur, startBlur, Time.deltaTime);
        //}

   
    }
}
