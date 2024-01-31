using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class AddOnBullet : MonoBehaviour
{
    private float near;
    private Vector3 _direction;
    private float MoveSpeed = 3;
    private void Start()
    {
       
    }
    private void Update()
    {
        TrackingPlayer();
        transform.position += new Vector3(_direction.x * MoveSpeed, _direction.y * MoveSpeed, 0) * Time.deltaTime;
        //transform.Translate(_direction * MoveSpeed * Time.deltaTime);
    }
    private void TrackingPlayer()
    {
        
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length == 0)
        {
            MoveSpeed = 15;
        }
        else
        {
            Vector3 EnemyPos = transform.position;

            for (int i = 0; i < enemies.Length; i++)
            {
                Transform enemiesPos = enemies[i].GetComponent<Transform>();
                float distance = Vector3.Distance(transform.position, enemiesPos.position);
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
                Vector3 currentDistance = EnemyPos - transform.position;
                currentDistance.Normalize();
                _direction = currentDistance;
            }
        }
    }
}
