using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUIView : MonoBehaviour
{
    void Start()
    {
        GameManager.Instance.LevelManager.LE.OnPlayerDeath += HandlePlayerDeath;
    }

    private void HandlePlayerDeath(LevelManager.DeathReason dr)
    {
        GetComponent<CanvasGroup>().DOFade(0, 0.2f);
    }

    void Update()
    {
        
    }
}
