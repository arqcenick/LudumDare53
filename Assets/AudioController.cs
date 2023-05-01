using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : PlayerComponent
{
    [SerializeField] private AudioSource _cargoHit;
    [SerializeField] private AudioSource _orderComplete;
    protected override void Start()
    {
        base.Start();
        player.PlayerEvents.OnCargoHit += HandleCargoHit;
        player.PlayerEvents.OnOrderCompleted += HandleOrderComplete;
    }

    private void HandleOrderComplete(OrderComponent obj)
    {
        _cargoHit.Play();
    }

    private void HandleCargoHit(Cargo obj)
    {
        _orderComplete.Play();
    }
}
