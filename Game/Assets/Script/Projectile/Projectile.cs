using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [HideInInspector]
    public float MoveSpeed = 2f;

    private Vector3 _direction;

    private float _rotation;

    [SerializeField]
    private float _lifeTime = 3f;

    private bool BossAttack;

    void Start()
    {
        Destroy(gameObject, _lifeTime);
        if (gameObject.layer == 7)
        {
            MoveSpeed = 4;
            _lifeTime = 0.75f;
        }
    }

    void FixedUpdate()
    {
        _lifeTime -= Time.deltaTime;

        transform.Translate(_direction * MoveSpeed * Time.deltaTime);


        EnemyDUpdate();
        TurnLeft();
        TurnRight();

        if (_lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void EnemyDUpdate()
    {
        if (gameObject.layer == 7)
        {
            _rotation = 200 * Time.deltaTime * 75;
            transform.Rotate(new Vector3(0, 0, _rotation * Time.deltaTime));
        }
    }
    private void TurnLeft()
    {
        if (gameObject.layer == 8)
        {
            _rotation = 80 * Time.deltaTime * 30;
            transform.Rotate(new Vector3(0, 0, _rotation * Time.deltaTime));
        }
    }
    private void TurnRight()
    {
        if (gameObject.layer == 9)
        {
            _rotation = 80 * Time.deltaTime * 30;
            transform.Rotate(new Vector3(0, 0, -_rotation * Time.deltaTime));
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "DestroyBullet":
                Destroy(gameObject);
                break;
            case "GuardBullet":
                if (!gameObject.CompareTag("PlayerBullet"))
                Destroy(gameObject);
                break;
            case "Enemy":
                if (gameObject.tag == "PlayerBullet")
                    Destroy(gameObject);
                break;
        }

    }

    public void SetDirection(Vector3 direction)
    {
        _direction = direction;
    }
}
