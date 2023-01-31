using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class TouchInputController : MonoBehaviour
{
    public Action OnClickOrTouch;
    public Action OnSwipeUp;
    public Action OnSwipeDown;

    [SerializeField] private float _deadzone;

    private float _touchInitY;
    private float _touchFinalY;

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
            _touchInitY = Input.mousePosition.y;

        else if(Input.GetMouseButtonUp(0))
        {
            _touchFinalY = Input.mousePosition.y;
            SelectInputAction();
        }
    }

    private void SelectInputAction()
    {
        float mouseDeltaY = _touchFinalY - _touchInitY;

        if (_touchFinalY > _touchInitY && Mathf.Abs(mouseDeltaY) > _deadzone)
            OnSwipeUp?.Invoke();

        else if(_touchFinalY < _touchInitY && Mathf.Abs(mouseDeltaY) > _deadzone)
            OnSwipeDown?.Invoke();

        else if (mouseDeltaY < _deadzone)
            OnClickOrTouch?.Invoke();

        _touchInitY = 0f;
        _touchFinalY = 0f;
    }
}
