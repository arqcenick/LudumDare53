using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DayUIView : MonoBehaviour
{


    [SerializeField] private TextMeshProUGUI _dayText;
    private Image _image;


    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    void Start()
    {
        GameManager.Instance.LevelManager.LE.OnDayProgressed += HandleDayProgression;
        GameManager.Instance.LevelManager.LE.OnDayPassed += HandleDayPass;
    }

    private void HandleDayPass(int day)
    {
        _dayText.text = day.ToString();
    }

    private void HandleDayProgression(float dayProgress)
    {
        _image.fillAmount = dayProgress;
    }

    void Update()
    {
        
        
    }
}
