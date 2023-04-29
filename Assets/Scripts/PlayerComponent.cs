using System;
using UnityEngine;

public class PlayerComponent : MonoBehaviour 
{
    protected Player player;
    protected virtual void Awake()
    {
    }

    protected virtual void Start()
    {
        player = GetComponentInParent<Player>();
    }
}
