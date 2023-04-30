using DG.Tweening;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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
                var lastCargo = cargos.Dequeue();
                if(lastCargo.Sequence != null)
                {
                    lastCargo.Sequence.Kill();
                }
                lastCargo.transform.parent = null;
                Vector3 position = lastCargo.transform.position - lastCargo.transform.forward * 0.5f;
                lastCargo.transform.DOJump(position , 5,1, 0.75f).OnComplete(() => { Destroy(lastCargo.gameObject); });

            }
            cargos.Enqueue(cargo);

            var cargoList = cargos.AsReadOnlyList();

            for (int i = 0; i < cargoList.Count; i++)
            {
                var c = cargoList[cargoList.Count - i - 1];
                var seq = DOTween.Sequence();
                c.transform.SetParent(_cargoHolders[i].transform);
                seq.Append(c.transform.DOLocalJump(_cargoHolders[i].CargoPosition, 5, 1, 0.8f));
                seq.Join(c.transform.DOLocalRotate(Vector3.zero, 1));

                c.Sequence = seq;
                //_cargoHolders[i].SetCargoView(cargoList[cargoList.Count - i - 1]);
            }
        }

        
    }
}
