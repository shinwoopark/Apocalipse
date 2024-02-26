using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatternA : MonoBehaviour
{
    public static GameInstance Instance;
    public Enemy Enemy;
    public float MoveSpeed;
    public float Amplitude;

    private bool movingUp = true;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (GameInstance.instance.CurrentStageLevel == 3)
        {
            Amplitude = 1.5f;
            MoveSpeed = 4;
        }
        float verticalMovement = MoveSpeed * Time.deltaTime;

        if (!Enemy.bFreeze)
        {
            if (movingUp && transform.position.x < startPosition.x + Amplitude)
            {
                transform.position += new Vector3(verticalMovement, 0f, 0f);
            }
            else if (!movingUp && transform.position.x > startPosition.x - Amplitude)
            {
                transform.position -= new Vector3(verticalMovement, 0f, 0f);
            }
            else
            {
                movingUp = !movingUp;
            }

            transform.position -= new Vector3(0f, MoveSpeed * Time.deltaTime, 0f);
        }    
    }
}
