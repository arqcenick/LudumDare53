using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScript : MonoBehaviour
{

    public void FadeIn()
    {
        GetComponent<Image>().DOFade(1, 0.5f);
    }

    public void FadeOut()
    {
        GetComponent<Image>().DOFade(0, 1.5f);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
