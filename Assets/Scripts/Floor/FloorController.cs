using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour
{
    [SerializeField] private FloorScrolling _floorScrolling = new FloorScrolling();
    [SerializeField] private TileSpawner _tileSpawner = new TileSpawner();

    private void Start()
    {
        DataSetup();
    }

    private void DataSetup()
    {
        _tileSpawner.SpawnerSetup();
    }
    
    public void FlipTilePositionAndChangeTile(GameObject tile)
    {
        _floorScrolling.ChangeTilePositionToOppositeSide(tile);
    }

    public void SpawnNewTile(GameObject tile)
    {
        _tileSpawner.SpawnTile(tile, _floorScrolling.LastTileSibling);
    }

    public void SetSpriteRendererOffset(SpriteRenderer newSR)
    {
        if(_floorScrolling.Offset == 0f)
            _floorScrolling.SetOffset(newSR.bounds.size.x);
    }

    public void StopScrolling()
    {
        _floorScrolling.StopTiles();
    }
}
