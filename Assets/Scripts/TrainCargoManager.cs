using System;
using System.Collections.Generic;
using Unity.VisualScripting;

public class TrainCargoManager : PlayerComponent
{

    private int _cargoCapacity;
    private Queue<Cargo> cargos = new Queue<Cargo>();
    private List<CargoHolder> _cargoHolders = new List<CargoHolder>();


    protected override void Awake()
    {
        base.Awake();

    }

    protected override void Start()
    {
        base.Start();
        player.PlayerEvents.OnCargoHolderAdded += HandleCargoHolderAddition;
        player.PlayerEvents.OnCargoCollected += HandleCargoCollection;
    }

    private void HandleCargoHolderAddition(CargoHolder cargoHolder)
    {
        _cargoCapacity++;
        _cargoHolders.Add(cargoHolder);
    }

    private void HandleCargoCollection(Cargo cargo)
    {

        if (_cargoCapacity > 0)
        {
            if (cargos.Count == _cargoCapacity)
            {
                cargos.Dequeue();
            }
            cargos.Enqueue(cargo);

            var cargoList = cargos.AsReadOnlyList();

            for (int i = 0; i < cargoList.Count; i++)
            {
                _cargoHolders[i].SetCargoView(cargoList[cargoList.Count - i - 1]);
            }
        }

        
    }
}
