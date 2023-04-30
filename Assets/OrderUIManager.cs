using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderUIManager : MonoBehaviour
{


    [SerializeField] private OrderUIView _orderUI;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.LevelManager.LE.OnOrderCreated += HandleOrderCreation;
        GameManager.Instance.LevelManager.LE.OnOrderCompleted += HandleOrderCompletion;

    }

    private void HandleOrderCompletion(OrderComponent orderComponent)
    {
        
    }

    private void HandleOrderCreation(OrderComponent orderComponent)
    {
        var orderUI = Instantiate<OrderUIView>(_orderUI, transform);
        RectTransform rect = orderUI.GetComponent<RectTransform>();
        rect.position = Camera.main.WorldToScreenPoint(orderComponent.transform.position);
        orderUI.SetOrderComponent(orderComponent);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
