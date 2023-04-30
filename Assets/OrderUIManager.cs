using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderUIManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.LevelManager.LE.OnOrderCreated += HandleOrderCreation;
        GameManager.Instance.LevelManager.LE.OnOrderCompleted += HandleOrderCompletion;

    }

    private void HandleOrderCompletion(OrderComponent obj)
    {
    }

    private void HandleOrderCreation(OrderComponent obj)
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
