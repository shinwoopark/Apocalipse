using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatternA : MonoBehaviour
{
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
        float verticalMovement = MoveSpeed * Time.deltaTime;

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
