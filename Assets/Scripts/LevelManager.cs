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


    private int _level;

    private Timer _cargoTimer = new Timer(1f);
    private Timer _buildingTimer = new Timer(3f);
    private Timer _levelTimer = new Timer(10f);


    private List<Cargo> _cargos = new List<Cargo>();
    private List<Cargo.CargoType> _possibleCargoTypes = new List<Cargo.CargoType>();
    private List<Building> _buildings = new List<Building>();

    private Player Player;

    private void Awake()
    {
        Player = FindAnyObjectByType<Player>();
    }

    void Start()
    {
        _level = 1;
        LE.OnDayPassed?.Invoke(_level);
        Player.PlayerEvents.OnCargoCollected += HandlePlayerCargoCollection;
        Player.PlayerEvents.OnOrderCompleted += HandlePlayerOrderCompleted;

        for (int i = 0; i < Enum.GetNames(typeof(Cargo.CargoType)).Length; i++)
        {
            var cargoType = (Cargo.CargoType)i;
            for (int j = 0; j < _cargoLimit; j++)
            {
                _possibleCargoTypes.Add(cargoType);
            }
        }
    }

    private void HandlePlayerCargoCollection(Cargo cargo)
    {
        _cargos.Remove(cargo);
        _possibleCargoTypes.Add(cargo.CurrentCargoType);
        
    }

    private void HandlePlayerOrderCompleted(OrderComponent obj)
    {
    }

    void Update()
    {
        _cargoTimer.Tick(Time.deltaTime);
        _buildingTimer.Tick(Time.deltaTime);
        _levelTimer.Tick(Time.deltaTime);

        LE.OnDayProgressed?.Invoke(_levelTimer.TimePassed / _levelTimer.TimeLimit);

        if (_cargoTimer.IsPassed && _possibleCargoTypes.Count > 0)
        {
            _cargoTimer.Reset();
            AddRandomCargoForLevel();
        }

        if(_buildingTimer.IsPassed )
        {
            _buildingTimer.Reset();
            AddBuildingForLevel();
        }

        if (_levelTimer.IsPassed)
        {
            _level++;
            _levelTimer.Reset();
            Player.PlayerEvents.OnDayPassed?.Invoke();
            LE.OnDayPassed?.Invoke(_level);
        }

    }

    private void AddBuildingForLevel()
    {
        Vector3 position;
        Quaternion rotation;

        int tries = 0;
        while (!TryFindBuildingTransform(out position, out rotation ) && tries < 50)
        {
            tries++;
        }
        if (tries < 50)
        {
            _buildings.Add(AddNewBuilding(position, rotation));
        }
    }

    private Building AddNewBuilding(Vector3 position, Quaternion rotation)
    {
        var building = Instantiate<Building>(PrefabManager.Instance.Building, position, rotation);
        AddOrderToBuildingForLevel(building);
        return building;
    }

    private void AddOrderToBuildingForLevel(Building building)
    {
        var orderComponent = building.GetComponent<OrderComponent>();

        orderComponent.OrderData = OrderManager.CreateNewOrder(Random.Range(1, 6));

        LE.OnOrderCreated?.Invoke(orderComponent);

    }

    private void AddRandomCargoForLevel()
    {
        var color = _possibleCargoTypes[Random.Range(0, _possibleCargoTypes.Count)];

        Vector3 possiblePosition;
        int tries = 0;
        while(!TryFindCargoPosition(out possiblePosition) && tries < 50)
        {
            tries++;
        }

        if(tries < 50)
        {
            _cargos.Add(AddNewCargo(possiblePosition, color));
            _possibleCargoTypes.Remove(color);

        }
    }

    private bool TryFindBuildingTransform(out Vector3 position, out Quaternion rotation)
    {
        position = Vector3.zero;
        rotation = Quaternion.identity;
        Ray ray = GetViewPortToWorldPositionRay();
        if (Physics.Raycast(ray, out var hit))
        {
            if (hit.collider.CompareTag("Blocker"))
            {
                return false;
            }
            else
            {
                position = hit.point;
                position.y = 1;

                rotation = Quaternion.Euler(0, Random.Range(0,4) * 90, 0);
                return true;
            }

        }
        return false;

    }

    private bool TryFindCargoPosition(out Vector3 result)
    {
        result = Vector3.zero;
        Ray ray = GetViewPortToWorldPositionRay();
        if (Physics.Raycast(ray, out var hit))
        {
            if (hit.collider.CompareTag("Blocker"))
            {
                return false;
            }
            else
            {
                result = hit.point;
                result.y = 0;
                return true;
            }

        }
        return false;
    }

    private static Ray GetViewPortToWorldPositionRay()
    {
        float randomX = Random.value * 0.9f + 0.05f;
        float randomY = Random.value * 0.9f + 0.05f;
        Vector3 viewportPosition = new Vector3(randomX, randomY, 0);
        var ray = Camera.main.ViewportPointToRay(viewportPosition);
        return ray;
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
