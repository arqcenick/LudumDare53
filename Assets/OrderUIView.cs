using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Cargo;

public class OrderUIView : MonoBehaviour
{
    [SerializeField] private List<Image> Indicators = new List<Image>();
    [SerializeField] private Image _dialogueBox;

    private RectTransform _rectTransform;
    private OrderComponent _orderComponent;
    public void SetOrderCargoTypes(List<Cargo.CargoType> cargoTypes)
    {
        if(cargoTypes.Count == 0)
        {
            _dialogueBox.enabled = false;
        }
        else
        {
            _dialogueBox.enabled = true;
        }

        for (int i = 0; i < Indicators.Count; i++)
        {
            if (i >= cargoTypes.Count)
            {
                Indicators[i].gameObject.SetActive(false);
            }
            else
            {
                Indicators[i].gameObject.SetActive(true);
            }
        }

        int count = 0;

        var reversed = new List<Cargo.CargoType>(cargoTypes);
        reversed.Reverse();

        foreach (var cargoType in reversed)
        {
            Color color = Color.white;

            switch (cargoType)
            {
                case CargoType.Red:
                    color = Color.red;
                    break;
                case CargoType.Blue:
                    color = Color.blue;
                    break;
                case CargoType.Green:
                    color = Color.green;
                    break;
            }

            Indicators[count].color = color;
            count++;
        }

        _dialogueBox.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Mathf.Max(2, cargoTypes.Count) * 80 + 20);

    }

    internal void SetOrderComponent(OrderComponent orderComponent)
    {
        _orderComponent = orderComponent;
        SetOrderCargoTypes(orderComponent.OrderData.CargoTypes);

    }

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    void Start()
    {

    }

    void Update()
    {
        _rectTransform.position = Camera.main.WorldToScreenPoint(_orderComponent.transform.position);
    }
}
