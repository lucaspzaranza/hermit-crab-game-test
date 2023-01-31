using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawnable : MonoBehaviour
{
    public static Action<Transform> OnTileEnabled;

    [SerializeField] private Transform _obstacleSpawnTransform;

    private bool _firstEnable = true;

    private void OnEnable()
    {
        // It'll prevent obstacle spawning right at the start of the game.
        if(!_firstEnable)
            OnTileEnabled?.Invoke(_obstacleSpawnTransform);
    }

    private void OnDisable()
    {
        _firstEnable = false;
    }
}
