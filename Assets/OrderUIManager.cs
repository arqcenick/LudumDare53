using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderUIManager : MonoBehaviour
{


    [SerializeField] private OrderUIView _orderUI;
    private Dictionary<OrderComponent, OrderUIView> _views = new Dictionary<OrderComponent, OrderUIView>();
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.LevelManager.LE.OnOrderCreated += HandleOrderCreation;
        GameManager.Instance.LevelManager.LE.OnOrderCompleted += HandleOrderCompletion;

    }

    private void HandleOrderCompletion(OrderComponent orderComponent)
    {
        Destroy(_views[orderComponent].gameObject);
    }

    private void HandleOrderCreation(OrderComponent orderComponent)
    {
        var orderUI = Instantiate<OrderUIView>(_orderUI, transform);
        RectTransform rect = orderUI.GetComponent<RectTransform>();
        rect.position = Camera.main.WorldToScreenPoint(orderComponent.transform.position);
        orderUI.SetOrderComponent(orderComponent);
        _views[orderComponent] = orderUI;   

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
