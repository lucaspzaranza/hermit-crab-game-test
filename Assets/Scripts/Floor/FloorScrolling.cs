using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FloorScrolling
{
    [SerializeField] private GameObject _lastTile;
    [SerializeField] private List<TileMover> _tileMovers;
    [SerializeField] private List<Scroller> _BGScrollers;

    private float _offset;
    public float Offset => _offset;

    public void SetOffset(float value)
    {
        _offset = value;
    }

    public void ChangeTilePositionToOppositeSide(GameObject tile)
    {
        float newX = _lastTile.transform.position.x + _offset;

        tile.transform.parent.position = new Vector2(
           newX, tile.transform.parent.position.y);

        _lastTile = tile.transform.parent.gameObject;

        tile.SetActive(false);
    }

    public void MoveTiles()
    {
        foreach (var tile in _tileMovers)
        {
            tile.MoveTile();
        }

        foreach (var scroller in _BGScrollers)
        {
            scroller.MoveOffset();
        }
    }

    public void StopTiles()
    {
        foreach (var tile in _tileMovers)
        {
            tile.StopTile();
        }

        foreach (var scroller in _BGScrollers)
        {
            scroller.StopOffset();
        }
    }
}
