using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatternC : MonoBehaviour
{
    public Enemy Enemy;
    public float MoveSpeed;
    public GameObject Projectile;
    public float ProjectileMoveSpeed;
    private bool _attack = false;
    private float _originPosition;

    void Start()
    {
        _originPosition = Random.Range(2, 5);
    }

    void Update()
    {
        if (Enemy.Freeze == false)
        {
            MoveUpdate();
            if (transform.position.y <= _originPosition)
            {
                MoveSpeed = 0;
                if (!_attack)
                {
                    _attack = true;
                    AttackUpdate();
                }
            }
        }

    }

    void AttackUpdate()
    {
        if (Enemy.Freeze == false)
        {
            if (_attack)
            {
                Invoke("AttackUpdate", 1.5f);
                GameObject manager = GameObject.Find("Managers");
                PlayerCharacter character = manager.GetComponent<GameManager>().PlayerCharacter;
                Vector3 playerPos = character.GetComponent<Transform>().position;
                Vector3 direction = playerPos - transform.position;
                direction.Normalize();
                var projectile = Instantiate(Projectile, transform.position, Quaternion.identity);
                projectile.GetComponent<Projectile>().SetDirection(direction);
                projectile.GetComponent<Projectile>().MoveSpeed = ProjectileMoveSpeed;
            }
        }
    }

    void MoveUpdate()
    {
        transform.position -= new Vector3(0f, MoveSpeed * Time.deltaTime, 0f);
    }
}
