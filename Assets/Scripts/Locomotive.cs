using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Carriage))]
public class Locomotive : PlayerComponent
{
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private float _speed = 6f;

    private List<Carriage> _carriages = new List<Carriage>();
    private Rigidbody _rigidBody;


    protected override void Start()
    {
        base.Start();
        _rigidBody = GetComponent<Rigidbody>();
        _carriages.Add(GetComponent<Carriage>());

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            AddCarriage();
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, Time.fixedDeltaTime * _rotationSpeed, 0);
        }
        else if(Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, -Time.fixedDeltaTime * _rotationSpeed, 0);
        }
        else
        {
            //_rigidBody.angularVelocity = Vector3.zero;
        }
        transform.Translate(Vector3.forward * Time.fixedDeltaTime * _speed);

    }

    public void AddCarriage()
    {
        var carriage = Instantiate<Carriage>(PrefabManager.Instance.Carriage);
        carriage.transform.parent = transform.parent;
        carriage.transform.position = transform.position - transform.forward * 2f;
        carriage.transform.rotation = transform.rotation;
        _carriages[_carriages.Count - 1].PulledCarriage = carriage;
        carriage.gameObject.SetActive(false);
        _carriages.Add(carriage);
        player.PlayerEvents.OnCargoHolderAdded?.Invoke(carriage.GetComponent<CargoHolder>());
    }

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.TryGetComponent<Cargo>(out var cargo);
        
        if (cargo != null)
        {

            Debug.Log("Cargo event fired!");
            player.PlayerEvents.OnCargoHit?.Invoke(cargo);
        }
    }


}
