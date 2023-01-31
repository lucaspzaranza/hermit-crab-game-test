using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCollisionDetector : MonoBehaviour
{
    public static Action<GameObject> OnTileTouchedFlipPos;
    public static Action<int> OnTileDamagePlayer;
    public static Action<bool> OnTileStopPlayer;

    [SerializeField] private bool _causeDamage;
    [SerializeField] private bool _canStopPlayer;
    [SerializeField] private int _damageAmount;

    public bool CauseDamage => _causeDamage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == GameTags.FlipTilePos && gameObject.tag == GameTags.Floor)
            OnTileTouchedFlipPos?.Invoke(gameObject);

        else if (other.tag == GameTags.Player && _causeDamage)
            OnTileDamagePlayer?.Invoke(_damageAmount);

        else if (other.tag == GameTags.StopCollider && _canStopPlayer)
            OnTileStopPlayer?.Invoke(true);

        else if (other.tag == GameTags.ObstacleDestroyer)
            Destroy(gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == GameTags.StopCollider && _canStopPlayer)
            OnTileStopPlayer?.Invoke(false);
    }
}
