using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class OrderComponent : MonoBehaviour
{
    internal OrderManager.OrderData OrderData;
    public bool IsOrderActive;
    private void Awake()
    {
        IsOrderActive = false;
    }
    
}
