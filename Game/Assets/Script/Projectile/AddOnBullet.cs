using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AddOnBullet : MonoBehaviour
{
    private float _near;

    private GameObject _target;
    private float _speed = 5f;

    private Vector3 _targetPos;
    private Rigidbody2D _rigidbody2D;
    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _target = GameObject.FindWithTag("Enemy");
    }
    private void Update()
    {
        
        FollowTarGet();
    }
    private void FollowTarGet()
    {
        if (_target == null)
        {
            FindTarget();
        }
        else if(_target != null)
        {
            _targetPos = (_target.transform.position - transform.position).normalized;
            _rigidbody2D.velocity = new Vector2(_targetPos.x * _speed, _targetPos.y * _speed);
        } 
    }
    private void FindTarget()
    {
        _target = GameObject.FindWithTag("Enemy");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "DestroyBullet":
                Destroy(gameObject);
                break;
            case "Enemy":
                    Destroy(gameObject);
                break;
        }

    }
}
