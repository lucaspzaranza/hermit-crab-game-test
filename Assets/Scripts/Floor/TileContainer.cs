using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileContainer : MonoBehaviour
{
    public static Action<SpriteRenderer> OnSpriteRendererSet;

    [SerializeField] private GameObject[] _tiles;
    [SerializeField] private SpriteRenderer _chosenTileSR;

    public GameObject[] Tiles => _tiles;
    public SpriteRenderer ChosenTileSR => _chosenTileSR;

    private void Start()
    {
        SetSpriteRenderer(0);
    }

    public void SetSpriteRenderer(int tileIndex)
    {
        _chosenTileSR = _tiles[tileIndex].GetComponent<SpriteRenderer>();
        OnSpriteRendererSet?.Invoke(_chosenTileSR);
    }
}
