using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMover : MonoBehaviour
{    
    [SerializeField] private float _speed;

    private float _initialSpeed;
    
    void Update()
    {
        transform.position = new Vector3(
            transform.position.x + (-_speed * Time.deltaTime), 
            transform.position.y,
            transform.position.z);
    }

    public void MoveTile()
    {
        _speed = _initialSpeed;
    }

    public void StopTile()
    {
        _initialSpeed = _speed;
        _speed = 0f;
    }

    private void OnDestroy()
    {
        _speed = _initialSpeed;
    }
}