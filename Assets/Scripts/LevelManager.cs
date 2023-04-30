using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public partial class LevelManager : MonoBehaviour
{
    public LevelEvents LE = new LevelEvents();

    [SerializeField] private float _timerLimit = 5;
    [SerializeField] private int _cargoLimit = 3;

    private float _timer;
    private int _level;

    private List<Cargo> _cargos = new List<Cargo>();
    private List<Cargo.CargoType> _possibleCargoTypes = new List<Cargo.CargoType>();
    void Start()
    {
        for (int i = 0; i < Enum.GetNames(typeof(Cargo.CargoType)).Length; i++)
        {
            var cargoType = (Cargo.CargoType)i;
            for (int j = 0; j < _cargoLimit; j++)
            {
                _possibleCargoTypes.Add(cargoType);
            }
        }
    }

    void Update()
    {
        _timer += Time.deltaTime;

        if( _timer > _timerLimit && _cargoLimit * Enum.GetNames(typeof(Cargo.CargoType)).Length > _cargos.Count)
        {
            _timer = 0;
            AddRandomCargoForLevel();
        }
    }

    private void AddRandomCargoForLevel()
    {
        var color = _possibleCargoTypes[Random.Range(0, _possibleCargoTypes.Count)];
        _possibleCargoTypes.Remove(color);

        Vector3 possiblePosition;
        int tries = 0;
        while(!TryFindCargoPosition(out possiblePosition) && tries < 50)
        {
            tries++;
        }

        if(tries < 50)
        {
            _cargos.Add(AddNewCargo(possiblePosition, color));
        }
    }

    private bool TryFindCargoPosition(out Vector3 result)
    {
        result = Vector3.zero;
        Vector3 viewportPosition = new Vector3(Random.value, Random.value, 0);
        var ray = Camera.main.ViewportPointToRay(viewportPosition);
        if (Physics.Raycast(ray, out var hit))
        {
            if(hit.collider.CompareTag("Blocker"))
            {
                return false;
            }
            else
            {
                result = hit.point;
                return true;
            }
            
        }
        return false;
    }

    private void AddRandomCargo()
    {
        Vector3 viewportPosition = new Vector3(Random.value, Random.value, 0);
        var ray = Camera.main.ViewportPointToRay(viewportPosition);
        if(Physics.Raycast(ray, out var hit))
        {
            AddNewCargo(hit.point, (Cargo.CargoType) Random.Range(0,3));
        }
    }

    private Cargo AddNewCargo(Vector3 position, Cargo.CargoType cargoType)
    {
        var cargo = Instantiate<Cargo>(PrefabManager.Instance.Cargo);
        cargo.transform.SetParent(transform, false);
        cargo.transform.position = position;
        cargo.SetCargoType(cargoType);
        return cargo;


    }
}
