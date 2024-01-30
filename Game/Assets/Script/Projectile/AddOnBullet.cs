using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class AddOnBullet : MonoBehaviour
{
    private float near;
    private Vector3 _direction;
    private float MoveSpeed = 2;
    private void Start()
    {
        TrackingPlayer1();
    }
    private void Update()
    {
        transform.position += new Vector3(_direction.x, _direction.y, 0) * MoveSpeed * Time.deltaTime;
        MoveSpeed += Time.deltaTime;
        //transform.Translate(_direction * MoveSpeed * Time.deltaTime);
    }
    private void TrackingPlayer1()
    {
        Invoke("TrackingPlayer1",0.1f);
        
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
            }
            _direction = EnemyPos - transform.position;
        }
    }
}
