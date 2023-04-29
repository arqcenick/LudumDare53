using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cargo : MonoBehaviour
{
    private Renderer _renderer;
    public CargoType CurrentCargoType => _type;
    private CargoType _type;
    public enum CargoType
    {
        Red,
        Blue,
        Green,
    }

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
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
}
