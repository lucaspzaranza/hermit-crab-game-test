using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMover : MonoBehaviour
{    
    [SerializeField] private float _speed;

    private float _initialSpeed;
    private bool _stop;
    
    void Update()
    {
        if (_stop) 
            return;

        transform.position = new Vector3(
            transform.position.x + (-_speed * Time.deltaTime), 
            transform.position.y,
            transform.position.z);
    }

    public void MoveTile()
    {
        _stop = false;
    }

    public void StopTile()
    {
        _stop = true;
    }
}