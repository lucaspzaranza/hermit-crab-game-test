using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.Profiling;
using UnityEngine;

[Serializable]
public class TileSpawner
{
    private const int MAX_LENGHT = 100;

    [SerializeField] private TileData[] _tileDatas;
    private int[] tilesIndexesByPercentage = new int[MAX_LENGHT];

    public void SpawnerSetup()
    {
        int counter = 0;

        // The tilesIndexesByPercentage is an array with 100 elements, 
        // and it'll be fulfilled according to the percentage of each tile data element.
        // For example, if an element has 30%, the array will fill 30 positions with the index
        // of the current element.

        for (int i = 0; i < _tileDatas.Length; i++)
        {
            int percentage = Mathf.FloorToInt(_tileDatas[i].SpawnPercentage * MAX_LENGHT);
            int indexLimit = counter + percentage;

            for (int j = counter; j < indexLimit; j++)
            {
                tilesIndexesByPercentage[j] = i;
                counter++;
            }
        }
    }

    public void SpawnTile(GameObject tile, GameObject lastTileSibling)
    {
        TileContainer container = tile.transform.parent.GetComponent<TileContainer>();

        int randomIndex = UnityEngine.Random.Range(0, MAX_LENGHT);
        int tileIndex = tilesIndexesByPercentage[randomIndex];

        bool tileCausesDamage = container.Tiles[tileIndex].GetComponent<TileCollisionDetector>().CausesDamage;

        // Prevent the next tile to be a damage tile and turn the game
        // impossible to win. This will avoid two or more consecutives acid or spike tiles 
        if (tileCausesDamage)  
        {
            TileContainer siblingContainer = lastTileSibling.GetComponent<TileContainer>();

            bool siblingTileCausesDamage = 
                siblingContainer.Tiles[siblingContainer.SelectedChildIndex]
                .GetComponent<TileCollisionDetector>().CausesDamage;

            while (siblingTileCausesDamage && tileCausesDamage)
            {
                randomIndex = UnityEngine.Random.Range(0, MAX_LENGHT);
                tileIndex = tilesIndexesByPercentage[randomIndex];

                tileCausesDamage = container.Tiles[tileIndex].GetComponent<TileCollisionDetector>().CausesDamage;
            }
        }
        
        container.SetSelectedChildIndex(tileIndex);
        container.Tiles[tileIndex].SetActive(true);
    }
}
