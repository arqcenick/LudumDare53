using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carriage : PlayerComponent
{

    private float _delayCoef = 0.3f;

    public Carriage PulledCarriage;
    private Queue<Marker> _markers = new Queue<Marker>();
    private bool _isAlive = true;
    private Rigidbody _rigidbody;
    private Vector3 _approximateVelocity;
    private Vector3 _lastPosition;

    protected override void Start()
    {
        base.Start();
        _isAlive = true;    
        player.PlayerEvents.OnPlayerDeathByCollision += HandlePlayerDeathByCollison;
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.velocity = Vector3.zero;


    }

    private void HandlePlayerDeathByCollison()
    {
        if(_isAlive)
        {
            Debug.Log("Death");
            _rigidbody.useGravity = true;
            _rigidbody.velocity = _approximateVelocity;
            var force = UnityEngine.Random.onUnitSphere;
            force.y = Mathf.Abs(force.y);
            _rigidbody.AddForce(force * 2f, ForceMode.Impulse);
            _isAlive = false;
        }

    }

    void FixedUpdate()
    {
        _approximateVelocity = (transform.position - _lastPosition) / Time.fixedDeltaTime;

        if (!_isAlive)
        {
            return;
        }

        _markers.Enqueue(new Marker(transform.position, transform.rotation, Time.fixedTimeAsDouble));
        var marker = _markers.Peek();

        if (PulledCarriage)
        {

            if (marker.Timestamp < Time.fixedTime - _delayCoef)
            {
                PulledCarriage.gameObject.SetActive(true);
                PulledCarriage.transform.position = marker.Position;
                PulledCarriage.transform.rotation = marker.Rotation;
                _markers.Dequeue();
            }
        }
        else
        {
            if (marker.Timestamp < Time.fixedTime + 10)
            {
                _markers.Dequeue();
            }
        }
        _lastPosition = transform.position;

    }

    public struct Marker
    {
        public Vector3 Position;
        public Quaternion Rotation;
        public double Timestamp;

        public Marker(Vector3 position, Quaternion rotation, double timestamp)
        {
            Position = position;
            Rotation = rotation;
            Timestamp = timestamp;
        }
    }
}
