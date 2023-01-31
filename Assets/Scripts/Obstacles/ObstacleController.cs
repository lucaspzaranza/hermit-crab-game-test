using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    [SerializeField] private List<GameObject> _obstacles = new List<GameObject>();
    [SerializeField] private Transform _obstaclesParent;
    [Range(0, 100)]
    [SerializeField] private int _spawnPercentage;

    public void SpawnObstacle(Transform obstacleTransform)
    {
        int randomPercentage = Random.Range(0, 100);
        if (randomPercentage > _spawnPercentage) // x% spawn chance
            return;

        int randomIndex = Random.Range(0, _obstacles.Count);
        GameObject newObstacle = Instantiate(_obstacles[randomIndex], obstacleTransform.position, Quaternion.identity);
        newObstacle.transform.SetParent(_obstaclesParent, false);

        ObstacleTile tile = newObstacle.GetComponent<ObstacleTile>();
        if (tile)
            newObstacle.transform.localPosition =
                new Vector3(newObstacle.transform.localPosition.x, 
                newObstacle.transform.localPosition.y + tile.YOffset, 
                _obstacles[randomIndex].transform.position.z);
    }

    public void MoveObstacles()
    {
        foreach (var obstacle in _obstacles)
        {
            obstacle.GetComponent<TileMover>().MoveTile();
        }
    }

    public void StopObstacles()
    {
        foreach (var obstacle in _obstacles)
        {
            obstacle.GetComponent<TileMover>().StopTile();
        }
    }
}
