using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour
{
    [SerializeField] private float _offsetRate;
    private Renderer _renderer;

    private float _initialOffset;

    void Start()
    {
        _renderer = GetComponent<Renderer>();

        if (!_renderer)
            Debug.LogError("Scroller Renderer not found. Please insert the script in a Game Object with a Renderer component.");
    }
     
    void Update()
    {
        _renderer.material.mainTextureOffset += new Vector2(_offsetRate * Time.deltaTime, 0f);
    }

    public void MoveOffset()
    {
        _offsetRate = _initialOffset;
    }

    public void StopOffset()
    {
        _initialOffset = _offsetRate;
        _offsetRate = 0f;
    }
}
