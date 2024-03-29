using System;
using System.Collections;
using UnityEngine;
using static Cargo;

public class CargoHolder : PlayerComponent
{
    public Vector3 CargoPosition;
    private Renderer _renderer;
    [SerializeField] public Transform CargoTransform;

    protected override void Awake()
    {
        base.Awake();
        _renderer = GetComponentInChildren<MeshRenderer>();

    }

    internal void SetCargoView(Cargo cargo)
    {
        switch (cargo.CurrentCargoType)
        {
            case CargoType.Red:
                _renderer.material.color = Color.red;
                break;
            case CargoType.Blue:
                _renderer.material.color = Color.blue;
                break;
            case CargoType.Green:
                _renderer.material.color = Color.green;
                break;
        }
    }
}
