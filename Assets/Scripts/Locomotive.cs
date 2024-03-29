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
        _isAlive = false;
        _rigidBody = GetComponent<Rigidbody>();
        _carriages.Add(GetComponent<Carriage>());
        player.PlayerEvents.OnPlayerDeathByCollision += HandlePlayerDeath;
        player.PlayerEvents.OnPlayerDeathByOutofBounds += HandlePlayerDeath;
        player.PlayerEvents.OnPlayerDeathByOrderFailure += HandlePlayerDeath;


        player.PlayerEvents.OnDayPassed += HandleDayPassed;


        Invoke("StartEngine", 1);


    }

    private void StartEngine()
    {
        _isAlive = true;
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
            player.PlayerEvents.OnPlayerTurning?.Invoke(ParticleEffectController.Direction.Right);
        }
        else if(Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, -Time.fixedDeltaTime * _rotationSpeed, 0);
            player.PlayerEvents.OnPlayerTurning?.Invoke(ParticleEffectController.Direction.Left);

        }
        else
        {
            player.PlayerEvents.OnPlayerTurning?.Invoke(ParticleEffectController.Direction.Forward);
        }
        transform.Translate(Vector3.forward * Time.fixedDeltaTime * _speed);
        var screenPos = Camera.main.WorldToViewportPoint(transform.position);
        if (screenPos.x > 1.05 || screenPos.x < -0.05 || screenPos.y < -0.05|| screenPos.y > 1.05)
        {
            player.PlayerEvents.OnPlayerDeathByOutofBounds();
        }
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
