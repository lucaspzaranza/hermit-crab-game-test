using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileContainer : MonoBehaviour
{
    public static Action<SpriteRenderer> OnSpriteRendererSet;

    [SerializeField] private GameObject[] _tiles;
    [SerializeField] private SpriteRenderer _chosenTileSR;

    private int _selectedChildIndex = 0;

    public GameObject[] Tiles => _tiles;
    public SpriteRenderer ChosenTileSR => _chosenTileSR;
    public int SelectedChildIndex => _selectedChildIndex;

    private void Start()
    {
        SetSpriteRenderer(0);
    }

    public void SetSpriteRenderer(int tileIndex)
    {
        _chosenTileSR = _tiles[tileIndex].GetComponent<SpriteRenderer>();
        OnSpriteRendererSet?.Invoke(_chosenTileSR);
    }

    public void SetSelectedChildIndex(int newIndex)
    {
        _selectedChildIndex = newIndex;
    }
}
