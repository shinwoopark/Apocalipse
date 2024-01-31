using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

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
        FindTarget();
        FollowTarGet();
    }
    private void FollowTarGet()
    {
        _targetPos = (_target.transform.position - transform.position).normalized;
        _rigidbody2D.velocity = new Vector2(_targetPos.x * _speed, _targetPos.y * _speed);
    }
    private void FindTarget()
    {
        
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length == 0)
        {
            return;
        }
        else
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                Transform enemiesPos = enemies[i].GetComponent<Transform>();
                float distance = Vector3.Distance(transform.position, enemiesPos.position);
                Mathf.Abs(distance);
                if (i == 0)
                {
                    _near = distance;
                    _target.transform.position = enemiesPos.position;
                }
                else if (_near > distance)
                {
                    _target.transform.position = enemiesPos.position;
                }
            }
        }
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
