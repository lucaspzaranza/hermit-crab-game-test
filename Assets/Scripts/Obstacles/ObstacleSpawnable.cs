using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawnable : MonoBehaviour
{
    public static Action<Transform> OnTileEnabled;

    [SerializeField] private Transform _obstacleSpawnTransform;

    private void OnEnable()
    {
        OnTileEnabled?.Invoke(_obstacleSpawnTransform);
    }
}
