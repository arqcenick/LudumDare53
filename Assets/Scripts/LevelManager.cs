using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class LevelManager : MonoBehaviour
{
    public LevelEvents LE = new LevelEvents();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            AddRandomCargo();
        }
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

    private void AddNewCargo(Vector3 position, Cargo.CargoType cargoType)
    {
        var cargo = Instantiate<Cargo>(PrefabManager.Instance.Cargo);
        cargo.transform.SetParent(transform, false);
        cargo.transform.position = position;
        cargo.SetCargoType(cargoType);


    }
}
