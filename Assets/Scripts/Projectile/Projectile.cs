using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Rigidbody2D _rb;
    private Animator _anim;
    private int _destroyedHash;

    private void Start()
    {
        _destroyedHash = Animator.StringToHash("Destroyed");
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>(); 
    }

    private void FixedUpdate()
    {
        if (_rb && _speed > 0f)
            _rb.velocity = new Vector2(_speed * Time.deltaTime, 0f);
    }

    public void StartDestroyAnimation()
    {
        _anim.SetTrigger(_destroyedHash);
    }

    public void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
