using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Controller which will manage all the other controllers.
/// </summary>
public class GameController : MonoBehaviour
{
    public static GameController Instance;

    [SerializeField] private bool _moveTiles;

    [SerializeField] private Player _player;
    [SerializeField] private UIController _UIController;
    [SerializeField] private ScoreController _scoreController;
    [SerializeField] private FloorController _floorController;
    [SerializeField] private ObstacleController _obstacleController;
    [SerializeField] private TouchInputController _touchInputController;

    public Player PlayerInstance => _player;
    public UIController UIController => _UIController;
    public ScoreController ScoreController => _scoreController;
    public bool MoveTiles => _moveTiles;

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void OnEnable()
    {
        EventHandlerSetup();
    }

    private void OnDisable()
    {
        EventHandlerReset();
    }

    private void Start()
    {
        if(!MoveTiles)
            StopTileMovement();
    }

    private void EventHandlerSetup()
    {
        PlayerInstance.OnPlayerTookDamage += HandleOnPlayerTookDamage;
        PlayerInstance.OnPlayerDeath += GameOver;
        ScoreController.OnScoreIncreased += HandleOnScoreIncreased;

        TileCollisionDetector.OnTileTouchedFlipPos += HandleOnTileTouchedFlip;
        TileContainer.OnSpriteRendererSet += HandleOnTileContainerSpriteRendererSet;

        TileCollisionDetector.OnTileDamagePlayer += DamagePlayer;
        TileCollisionDetector.OnTileStopPlayer += SetStopPlayer;
        TileCollisionDetector.OnObstacleDestroyed += HandleOnObstacleDestroyed;

        ObstacleSpawnable.OnTileEnabled += HandleOnObstacleTileEnabled;

        _touchInputController.OnClickOrTouch += HandleClickOrTouch;
        _touchInputController.OnSwipeUp += HandleOnSwipeUp;
        _touchInputController.OnSwipeDown += HandleOnSwipeDown;
    }

    private void EventHandlerReset()
    {
        PlayerInstance.OnPlayerTookDamage -= HandleOnPlayerTookDamage;
        PlayerInstance.OnPlayerDeath -= GameOver;
        ScoreController.OnScoreIncreased -= HandleOnScoreIncreased;

        TileCollisionDetector.OnTileTouchedFlipPos -= HandleOnTileTouchedFlip;
        TileContainer.OnSpriteRendererSet -= HandleOnTileContainerSpriteRendererSet;

        TileCollisionDetector.OnTileDamagePlayer -= DamagePlayer;
        TileCollisionDetector.OnTileStopPlayer -= SetStopPlayer;
        TileCollisionDetector.OnObstacleDestroyed -= HandleOnObstacleDestroyed;

        ObstacleSpawnable.OnTileEnabled -= HandleOnObstacleTileEnabled;

        _touchInputController.OnClickOrTouch -= HandleClickOrTouch;
        _touchInputController.OnSwipeUp -= HandleOnSwipeUp;
        _touchInputController.OnSwipeDown -= HandleOnSwipeDown;
    }

    private void HandleOnScoreIncreased(int score)
    {
        PlayerInstance.IncreasePlayerScore(score);
        UIController.UpdateScoreText(PlayerInstance.Score);
    }

    private void HandleOnPlayerTookDamage(int newHealth)
    {
        UIController.UpdateHealthBar(newHealth);
    }

    private void HandleOnTileTouchedFlip(GameObject tile)
    {
        _floorController.FlipTilePositionAndChangeTile(tile);
        _floorController.SpawnNewTile(tile);
    }

    private void HandleOnTileContainerSpriteRendererSet(SpriteRenderer newSR)
    {
        _floorController.SetSpriteRendererOffset(newSR);
    }

    private void GameOver()
    {
        StopTileMovement();
        _touchInputController.gameObject.SetActive(false);
        PlayerInstance.GetComponent<Player>().enabled = false;
        UIController.SetGameOverActivation(true);
    }

    private void StopTileMovement()
    {
        _obstacleController.StopObstacles();
        _floorController.StopScrolling();
    }
    
    private void DamagePlayer(int amount)
    {
        PlayerInstance.TakeDamage(amount);
    }

    private void SetStopPlayer(bool value)
    {
        if(!value)
            PlayerInstance.SetStopPlayerAnimation(false);
        else 
        if(PlayerInstance.CanJump && PlayerInstance.IsRunning)
            PlayerInstance.SetStopPlayerAnimation(true);
    }

    private void HandleOnObstacleTileEnabled(Transform obstacleTransform)
    {        
        _obstacleController.SpawnObstacle(obstacleTransform);
    }

    private void HandleOnObstacleDestroyed(GameObject obstacle)
    {

    }

    private void HandleClickOrTouch()
    {
        print("Click or touch. FIRE!");
    }

    private void HandleOnSwipeUp()
    {
        if (PlayerInstance.CanJump)
            PlayerInstance.Jump();
    }

    private void HandleOnSwipeDown()
    {
        if (PlayerInstance.CanJump)
            PlayerInstance.Dash();
    }
}
