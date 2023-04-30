using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carriage : PlayerComponent
{

    private float _delayCoef = 0.3f;

    public Carriage PulledCarriage;
    private Queue<Marker> _markers = new Queue<Marker>();

    protected override void Start()
    {
        base.Start();
        
    }

    void FixedUpdate()
    {
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
