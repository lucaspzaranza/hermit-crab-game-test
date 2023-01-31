using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

/// <summary>
/// A class to store all the string hashes to use on the animator parameters.
/// It'll be useful for optimization purposes.
/// </summary>
public class PlayerAnimatorHashes
{
    private int _jump = Animator.StringToHash("Jump");
    public int Jump => _jump;

    private int _stop = Animator.StringToHash("Stop");
    public int Stop => _stop;

    private int _dash = Animator.StringToHash("Dash");
    public int Dash => _dash;

    private int _death = Animator.StringToHash("Death");
    public int Death => _death;
}

public class Player : MonoBehaviour
{
    private const int MAX_HEALTH = 100;
    private const int MAX_SCORE = 9999;
    private const float STOP_COLLIDER_DISTANCE = 30f;
    private const float COLLIDER_DASH_SIZE = 0.91f;
    private const float COLLIDER_OFFSET_SIZE = -1.81f;

    public Action<int> OnPlayerTookDamage;
    public Action OnPlayerDeath;

    [SerializeField] private float _jumpForce;
    [Range(0, MAX_HEALTH)]
    [SerializeField] private int _health;
    [SerializeField] private bool _invincible = false;
    [SerializeField] private float _returnToOriginSpeed;
    [SerializeField] private float _originMinDistance;
    [SerializeField] private bool _canJump = true;
    [SerializeField] private bool _isRunning = true;

    private Rigidbody2D _rb;
    private Collider2D _stopCollider;
    private BoxCollider2D _playerCollider;
    private Animator _anim;
    private PlayerAnimatorHashes _animHashes = new PlayerAnimatorHashes();
    private Blinker _blinker;
    private Vector2 _origin;
    private Vector2 _stopColliderOriginPos;
    private Vector2 _playerColliderOriginalSize;
    private Vector2 _playerColliderOriginalOffset;
    private int _score;

    public int Score => _score;
    public bool CanJump => _canJump;
    public bool IsRunning => _isRunning;

    private void OnDisable()
    {
        _blinker.OnBlinkStopped -= HandleOnBlinkerStopped;
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _stopCollider = transform.GetChild(0).GetComponent<Collider2D>();
        _playerCollider = GetComponent<BoxCollider2D>();
        _anim = GetComponent<Animator>();
        _blinker = GetComponent<Blinker>();
        _blinker.OnBlinkStopped += HandleOnBlinkerStopped;

        _health = MAX_HEALTH;
        _origin = transform.position;
    }

    void Update()
    {
        // Temp
        if (Input.GetKeyDown(KeyCode.Space))
            TakeDamage(100);

        if (_isRunning && _origin.x - transform.position.x > _originMinDistance)
            ReturnToCenter();
    }

    public void Jump()
    {
        _canJump = false;
        _anim.SetTrigger(_animHashes.Jump);
        _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }

    public void Dash()
    {
        _playerColliderOriginalSize = _playerCollider.size;
        _playerColliderOriginalOffset = _playerCollider.offset;

        _playerCollider.size = new Vector2(_playerCollider.size.x, COLLIDER_DASH_SIZE);
        _playerCollider.offset = new Vector2(_playerCollider.offset.x, COLLIDER_OFFSET_SIZE);

        _canJump = false;
        _stopColliderOriginPos = _stopCollider.gameObject.transform.localPosition;
        _stopCollider.gameObject.transform.localPosition = 
            new Vector2(_stopColliderOriginPos.x, _stopColliderOriginPos.y + STOP_COLLIDER_DISTANCE);

        _anim.SetBool(_animHashes.Stop, false);
        _anim.SetTrigger(_animHashes.Dash);
    }

    public void EndDash()
    {
        _canJump = true;
        _stopCollider.gameObject.transform.localPosition = _stopColliderOriginPos;
        _playerCollider.offset = _playerColliderOriginalOffset;
        _playerCollider.size = _playerColliderOriginalSize;
    }

    public void TakeDamage(int amount)
    {
        if(!_invincible)
        {
            _health = Mathf.Clamp(_health - amount, 0, MAX_HEALTH);
            if (_health > 0)
            {
                _blinker.Blink();
                _invincible = true;
                OnPlayerTookDamage?.Invoke(_health);
            }
            else
            {
                OnPlayerDeath?.Invoke();
                _anim.SetTrigger(_animHashes.Death);
            }
        }
    }

    private void HandleOnBlinkerStopped()
    {
        _invincible = false;
    }

    /// <summary>
    /// Set the stop animation ON/OFF;
    /// </summary>
    /// <param name="value">Value to set on the animator trigger param to toggle stop animation.</param>
    public void SetStopPlayerAnimation(bool value)
    {
        _anim.SetBool(_animHashes.Stop, value);
        _isRunning = !value;
    }

    public void IncreasePlayerScore(int score)
    {
        int newSore = _score + score;
        _score = Mathf.Clamp(newSore, 0, MAX_SCORE);
    }

    private void ReturnToCenter()
    {
        transform.position = Vector2.MoveTowards(transform.position, _origin, _returnToOriginSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == GameTags.Floor || collision.gameObject.tag == GameTags.Obstacle)
            _canJump = true;
    }
}
