using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddOn : MonoBehaviour
{
    public GameObject Projectile_gb;
    public float NoEnemyProjectileMoveSpeed = 15;
    public float ProjectileMoveSpeed = 3;
    private float near;

    public Transform FollowTransform;

    void Start()
    {
        ShootCycleTime();
    }

    void Update()
    {
        FollowPlayer();
    }
    private void FollowPlayer()
    {
        transform.position = Vector3.Lerp(gameObject.transform.position, FollowTransform.transform.position, Time.deltaTime * 3);
    }
    private void ShootCycleTime()
    {
        Invoke("ShootCycleTime", 3);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length == 0)
        {
            GameObject instance = Instantiate(Projectile_gb, transform.position, Quaternion.identity);
            Projectile projectile = instance.GetComponent<Projectile>();
            if (projectile != null)
            {
                projectile.MoveSpeed = NoEnemyProjectileMoveSpeed;
                projectile.SetDirection(Vector3.up);
            }
        }
        else
        {
            Vector3 EnemyPos = transform.position;

            for (int i = 0; i < enemies.Length; i++)
            {
                Transform enemiesPos = enemies[i].GetComponent<Transform>();
                float distance = Vector3.Distance(gameObject.transform.position, enemiesPos.position);
                Mathf.Abs(distance);
                if (i == 0)
                {
                    near = distance;
                    EnemyPos = enemiesPos.position;
                }
                else if (near > distance)
                {
                    EnemyPos = enemiesPos.position;
                }
            }
            GameObject instance = Instantiate(Projectile_gb, transform.position, Quaternion.identity);
            Projectile projectile = instance.GetComponent<Projectile>();
            if (projectile != null)
            {
                Vector3 direction = EnemyPos - transform.position;
                projectile.MoveSpeed = ProjectileMoveSpeed;
                projectile.SetDirection(direction);
            }
        }
    }
}