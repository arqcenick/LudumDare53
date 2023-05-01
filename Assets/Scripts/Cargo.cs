using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cargo : MonoBehaviour
{

    [SerializeField] private Collider[] _colliders;

    [SerializeField] public List<Color> _colors = new List<Color>();

    public CargoType CurrentCargoType => _type;
    private CargoType _type;
    private Renderer _renderer;
    public Sequence Sequence;

    public enum CargoType
    {
        Red,
        Blue,
        Green,
        None,
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
        _renderer.material.color = _colors[(int)type];
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
