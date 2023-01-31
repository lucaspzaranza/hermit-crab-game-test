using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public Action<int> OnScoreIncreased;

    [SerializeField] private float _increaseRate;
    [SerializeField] private int _scoreToIncrease;

    private float _timeCounter;

    void Update()
    {
        _timeCounter += Time.deltaTime;
        if(_timeCounter >= _increaseRate)
        {
            OnScoreIncreased?.Invoke(_scoreToIncrease);
            _timeCounter = 0;
        }
    }
}
