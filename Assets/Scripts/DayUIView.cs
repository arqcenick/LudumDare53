using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayUIView : MonoBehaviour
{

    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    void Start()
    {
        GameManager.Instance.LevelManager.LE.OnDayProgressed += HandleDayProgression;
    }

    private void HandleDayProgression(float dayProgress)
    {
        _image.fillAmount = dayProgress;
    }

    void Update()
    {
        
        
    }
}
