using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cargo : MonoBehaviour
{

    [SerializeField] private Collider[] _colliders;

    public CargoType CurrentCargoType => _type;
    private CargoType _type;
    private Renderer _renderer;
    public Sequence Sequence;

    public enum CargoType
    {
        Red,
        Blue,
        Green,
    }

    private void Awake()
    {
        _renderer = GetComponentInChildren<Renderer>();
    }

    void Start()
    {
        
    }

    public void SetCargoType(CargoType type)
    {
        switch (type)
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
        _type = type;
    }

    internal void SetCollidersEnabled(bool isEnabled)
    {
        foreach (var collider in _colliders)
        {
            collider.enabled = isEnabled;
        }
    }
}
