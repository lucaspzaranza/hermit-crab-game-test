using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TileData", menuName = "ScriptableObjects/TileData", order = 0)]
public class TileData : ScriptableObject 
{
    [Range(0,1)]
    [SerializeField] private float _spawnPercentage;

    public float SpawnPercentage => _spawnPercentage;
}
