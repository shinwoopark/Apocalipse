using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatternD : MonoBehaviour
{
    public Enemy Enemy;
    public float MoveSpeed;
    public GameObject Projectile;
    public float ProjectileMoveSpeed;

    private Vector3 _direction;

    void Update()
    {
        if (Enemy.Freeze == false)
        {
            PlayerPosUpdate();
            MoveUpdate();
        }
    }
    public void Attack()
    {
        if (Enemy.Freeze == false)
        {
            Vector3 position = transform.position;
            for (int i = 0; i < 360; i += 60)
            {
                float angle = i * Mathf.Deg2Rad;
                Vector3 direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);

                ShootProjectile(position, direction);
            }

            Destroy(gameObject);
        }
    }
    public void ShootProjectile(Vector3 position, Vector3 direction)
    {
        GameObject instance = Instantiate(Projectile, position, Quaternion.identity);
        Projectile projectile = instance.GetComponent<Projectile>();

        if (projectile != null)
        {
            projectile.MoveSpeed = ProjectileMoveSpeed;
            projectile.SetDirection(direction.normalized);
        }
    }
    void MoveUpdate()
    {
        transform.Translate(_direction * MoveSpeed * Time.deltaTime);
    }
    void PlayerPosUpdate()
    {
        GameObject manager = GameObject.Find("Managers");
        PlayerCharacter character = manager.GetComponent<GameManager>().PlayerCharacter;
        Vector3 playerPos = character.GetComponent<Transform>().position;
        Vector3 direction = playerPos - transform.position;
        direction.Normalize();
        _direction = direction;
    }
}
