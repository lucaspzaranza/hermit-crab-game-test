using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinker : MonoBehaviour
{
    public Action OnBlinkStopped;
    
    [SerializeField] private float _duration;
    [SerializeField] private int _speed;
    [SerializeField] private bool _blink;

    private SpriteRenderer _sr;
    private float _timeCounter;
    private int _frameCounter;

    private void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(_blink)
        {
            _frameCounter++;

            if(_timeCounter <= _duration)
            {
                if(_frameCounter == _speed)
                {
                    _sr.enabled = !_sr.enabled;
                    _frameCounter = 0;
                }

                _timeCounter += Time.deltaTime;
            }
            else
            {
                _sr.enabled = true;
                _blink = false;
                _timeCounter = 0f;
                _frameCounter = 0;

                OnBlinkStopped?.Invoke();
            }
        }
    }

    public void Blink()
    {
        _blink = true;
    }
}
