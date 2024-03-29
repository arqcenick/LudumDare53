﻿using System;
using UnityEngine;

public struct PlayerEvents
{
    public Action<Cargo> OnCargoHit;
    public Action<Cargo> OnCargoCollected;
    public Action<CargoHolder> OnCargoHolderAdded;
    public Action<OrderComponent> OnOrderCompleted;
    public Action<OrderComponent> OnOrderDropoffPointHit;
    public Action OnPlayerDeathByCollision;
    public Action OnPlayerDeathByOrderFailure;

    public Action<ParticleEffectController.Direction> OnPlayerTurning;
    public Action OnPlayerDeathByOutofBounds;
    public Action OnDayPassed;


}
