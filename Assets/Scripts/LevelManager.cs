using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    
    // Start is called before the first frame update
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
            AddNewCargo(hit.point);
        }
    }

    private void AddNewCargo(Vector3 position)
    {
        var cargo = Instantiate<Cargo>(PrefabManager.Instance.Cargo);
        cargo.transform.SetParent(transform, false);
        cargo.transform.position = position;


    }
}
