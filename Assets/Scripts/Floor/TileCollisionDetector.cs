using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCollisionDetector : MonoBehaviour
{
    public static Action<GameObject> OnTileTouchedFlipPos;
    public static Action<int> OnTileDamagePlayer;
    public static Action<bool> OnTileStopPlayer;
    public static Action<GameObject> OnObstacleDestroyed;
    public static Action<GameObject> OnProjectileDestroyed;

    [SerializeField] private bool _causesDamage;
    [SerializeField] private bool _destructible;
    [SerializeField] private bool _canStopPlayer;
    [SerializeField] private int _damageAmount;
    [SerializeField] private int _health;

    private Animator _anim;
    private int _takeDmgHash;

    public bool CausesDamage => _causesDamage;

    private void Start()
    {
        _takeDmgHash = Animator.StringToHash("TakeDamage");
        _anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == GameTags.FlipTilePos && gameObject.tag == GameTags.Floor)
            OnTileTouchedFlipPos?.Invoke(gameObject);

        else if (other.tag == GameTags.Player && _causesDamage)
            OnTileDamagePlayer?.Invoke(_damageAmount);

        else if (other.tag == GameTags.StopCollider && _canStopPlayer)
            OnTileStopPlayer?.Invoke(true);

        else if (other.tag == GameTags.ObstacleDestroyer)
            OnObstacleDestroyed?.Invoke(gameObject);

        else if (tag != GameTags.Untagged && other.tag == GameTags.Projectile)
        {
            if (_destructible)
            {
                _health--;
                if (_health > 0 && _anim)
                    _anim.SetTrigger(_takeDmgHash);
                else
                    OnObstacleDestroyed?.Invoke(gameObject);
            }

            OnProjectileDestroyed?.Invoke(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == GameTags.StopCollider && _canStopPlayer)
            OnTileStopPlayer?.Invoke(false);
    }
}
