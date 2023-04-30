using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class TrainCargoManager : PlayerComponent
{

    private int _cargoCapacity;
    private List<Cargo> cargos = new List<Cargo>();
    private List<CargoHolder> _cargoHolders = new List<CargoHolder>();


    protected override void Awake()
    {
        base.Awake();

    }

    protected override void Start()
    {
        base.Start();
        player.PlayerEvents.OnCargoHolderAdded += HandleCargoHolderAddition;
        player.PlayerEvents.OnCargoHit += HandleCargoHit;
        player.PlayerEvents.OnOrderDropoffPointHit += HandleCargoDropoffHit;
    }

    private void HandleCargoDropoffHit(OrderComponent orderComponent)
    {
        var orderTypes = orderComponent.OrderData.CargoTypes;
        if(cargos.Count < orderTypes.Count)
        {
            return;
        }
        bool finalResult = false;
        int matchingIndex = -1;

        for (int i = 0; i < cargos.Count - orderTypes.Count + 1; i++)
        {
            bool result = true;
            for (int j = 0; j < orderTypes.Count; j++)
            {
                result &= cargos[i+j].CurrentCargoType == orderTypes[j];
            }

            if(result)
            {
                matchingIndex = i;
            }

            finalResult |= result;
        }

        if(!finalResult)
        {
            return;
        }

        List<Cargo> removeList = new List<Cargo>();

        for (int i = matchingIndex; i < matchingIndex + orderTypes.Count; i++)
        {
            var deliveredCargo = cargos[i];
            if (deliveredCargo.Sequence != null)
            {
                deliveredCargo.Sequence.Kill();
            }
            deliveredCargo.transform.parent = null;
            Vector3 position = orderComponent.transform.position;
            deliveredCargo.transform.DOJump(position, 5, 1, 0.75f).OnComplete(() => { Destroy(deliveredCargo.gameObject); });
            removeList.Add(deliveredCargo);
        }

        //for (int i = matchingIndex-1; i >= 0; i--)
        //{
        //    var c = cargos[i];
        //    var seq = DOTween.Sequence();
        //    c.transform.SetParent(_cargoHolders[i].transform);
        //    seq.Append(c.transform.DOLocalJump(_cargoHolders[i].CargoPosition, 5, 1, 0.8f));
        //    seq.Join(c.transform.DOLocalRotate(Vector3.zero, 1));

        //    c.Sequence = seq;
        //}

        foreach (var cargo in removeList)
        {
            cargos.Remove(cargo);
        }

        for (int i = 0; i < matchingIndex; i++)
        {
            var c = cargos[cargos.Count - i - 1];
            var seq = DOTween.Sequence();
            c.transform.SetParent(_cargoHolders[i].transform);
            seq.Append(c.transform.DOLocalJump(_cargoHolders[i].CargoPosition, 5, 1, 0.8f));
            seq.Join(c.transform.DOLocalRotate(Vector3.zero, 1));

            c.Sequence = seq;
        }



    }

    private void HandleCargoHolderAddition(CargoHolder cargoHolder)
    {
        _cargoCapacity++;
        _cargoHolders.Add(cargoHolder);
    }

    private void HandleCargoHit(Cargo cargo)
    {

        if (_cargoCapacity > 0)
        {
            cargo.SetCollidersEnabled(false);

            if (cargos.Count == _cargoCapacity)
            {
                var lastCargo = cargos[0];
                cargos.RemoveAt(0);
                if (lastCargo.Sequence != null)
                {
                    lastCargo.Sequence.Kill();
                }
                lastCargo.transform.parent = null;
                Vector3 position = lastCargo.transform.position - lastCargo.transform.forward * 0.5f;
                lastCargo.transform.DOJump(position , 5,1, 0.75f).OnComplete(() => { Destroy(lastCargo.gameObject); });

            }
            cargos.Add(cargo);

           

            for (int i = 0; i < cargos.Count; i++)
            {
                var c = cargos[cargos.Count - i - 1];
                var seq = DOTween.Sequence();
                c.transform.SetParent(_cargoHolders[i].transform);
                seq.Append(c.transform.DOLocalJump(_cargoHolders[i].CargoPosition, 5, 1, 0.8f));
                seq.Join(c.transform.DOLocalRotate(Vector3.zero, 1));

                c.Sequence = seq;
            }

            player.PlayerEvents.OnCargoCollected?.Invoke(cargo);
        }

        
    }
}
