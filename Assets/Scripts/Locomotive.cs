using System;
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

    private bool _isAlive = true;


    protected override void Start()
    {
        base.Start();
        _isAlive = true;
        _rigidBody = GetComponent<Rigidbody>();
        _carriages.Add(GetComponent<Carriage>());
        player.PlayerEvents.OnPlayerDeathByCollision += HandlePlayerDeath;
        player.PlayerEvents.OnDayPassed += HandleDayPassed;

        AddCarriage();

    }

    private void HandleDayPassed()
    {
        AddCarriage();
    }

    private void HandlePlayerDeath()
    {
        enabled = false;
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
        if(!_isAlive)
        {
            return;
        }
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

        if(other.gameObject.CompareTag("Cargo"))
        {
            other.gameObject.TryGetComponent<Cargo>(out var cargo);

            if (cargo != null)
            {

                Debug.Log("Cargo event fired!");
                player.PlayerEvents.OnCargoHit?.Invoke(cargo);
            }
        }

        else if(other.gameObject.CompareTag("CargoDropoff"))
        {
            var orderComponent = other.gameObject.GetComponentInParent<OrderComponent>();
            if(orderComponent != null)
            {
                player.PlayerEvents.OnOrderDropoffPointHit?.Invoke(orderComponent);
            }
        }



    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Death"))
        {
            player.PlayerEvents.OnPlayerDeathByCollision?.Invoke();
        }
        else if(collision.gameObject.CompareTag("Carriage"))
        {
            player.PlayerEvents.OnPlayerDeathByCollision?.Invoke();

        }
    }


}
