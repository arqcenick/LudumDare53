using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ParticleEffectController : PlayerComponent
{
    public enum Direction
    {
        Left,
        Right,
        Forward,
    }

    private List<TrailRenderer> trailRenderers = new List<TrailRenderer>();
    [SerializeField] private Transform _carriage;
    private bool _isAlive;

    protected override void Start()
    {
        base.Start();
        _isAlive  = true;
        player.PlayerEvents.OnPlayerDeathByCollision += HandleDeath;
        player.PlayerEvents.OnPlayerTurning += HandleTurn;
        trailRenderers = GetComponentsInChildren<TrailRenderer>().ToList();
    }

    private void HandleTurn(Direction direction)
    {
        if (direction == Direction.Right)
        {

            var left = trailRenderers[0];
            var right = trailRenderers[1];
            left.widthMultiplier = 0.5f;
            right.emitting = false;
            left.emitting = true;

        }
        else if (direction == Direction.Forward)
        {
            foreach (var trailRenderer in trailRenderers)
            {
                trailRenderer.emitting = false;
            }
        }
        else
        {
            var left = trailRenderers[0];
            var right = trailRenderers[1];
            right.widthMultiplier = 0.5f;
            left.emitting = false;
            right.emitting = true;


        }
    }

    private void HandleDeath()
    {
        _isAlive = false;

    }

    // Update is called once per frame
    void Update()
    {

    }
}
