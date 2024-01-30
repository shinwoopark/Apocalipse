using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossA : MonoBehaviour
{
    public GameObject Projectile_gb, Projectile2_gb;
    public float ProjectileMoveSpeed = 5.0f;
    public float FireRate = 2.0f;
    public float MoveSpeed = 2.0f;
    public float MoveDistance = 5.0f;

    private int _currentPatternIndex = 0;
    private bool _movingRight = true;
    private bool _bCanMove = false;
    private bool _move = true;
    private Vector3 _originPosition;

    private void Start()
    {
        _originPosition = transform.position;
        StartCoroutine(MoveDownAndStartPattern());
    }

    private IEnumerator MoveDownAndStartPattern()
    {
        while (transform.position.y > _originPosition.y - 3f)
        {
            transform.Translate(Vector3.down * MoveSpeed * Time.deltaTime);
            yield return null;
        }

        _bCanMove = true;
        InvokeRepeating("NextPattern", 2.0f, FireRate);
    }

    private void Update()
    {
        if (_bCanMove)
            MoveSideways();
    }

    private void NextPattern()
    {
        _currentPatternIndex = (_currentPatternIndex + 1) % 6;

        switch (_currentPatternIndex)
        {
            case 0:
                Pattern1();
                break;
            case 1:
                Pattern2();
                break;
            case 2:
                StartCoroutine(Pattern3());
                break;
            case 3:
                Pattern4();
                break;
            case 4:
                StartCoroutine(Pattern5());
                break;
            case 5:
                Pattern6();
                break;
        }
    }

    private void MoveSideways()
    {
        if (_move)
        {
            if (_movingRight)
            {
                transform.Translate(Vector3.right * MoveSpeed * Time.deltaTime);
                if (transform.position.x > MoveDistance)
                {
                    _movingRight = false;
                }
            }
            else
            {
                transform.Translate(Vector3.left * MoveSpeed * Time.deltaTime);
                if (transform.position.x < -MoveDistance)
                {
                    _movingRight = true;
                }
            }
        }
    }

    private void StartMovingSideways()
    {
        StartCoroutine(MovingSidewaysRoutine());
    }

    private IEnumerator MovingSidewaysRoutine()
    {
        while (true)
        {
            MoveSideways();
            yield return null;
        }
    }

    public void ShootProjectile(Vector3 position, Vector3 direction, int Bullet)
    {
        if (Bullet == 2)
        {
            GameObject instance = Instantiate(Projectile2_gb, position, Quaternion.identity);
            Projectile projectile = instance.GetComponent<Projectile>();

            if (projectile != null)
            {
                projectile.MoveSpeed = ProjectileMoveSpeed;
                projectile.SetDirection(direction.normalized);
            }
        }
        else
        {
            GameObject instance = Instantiate(Projectile_gb, position, Quaternion.identity);
            Projectile projectile = instance.GetComponent<Projectile>();

            if (projectile != null)
            {
                projectile.MoveSpeed = ProjectileMoveSpeed;
                projectile.SetDirection(direction.normalized);
            }
        }


    }

    private void Pattern1()
    {
        int numBullets1 = 30;
        float angleStep1 = 360.0f / numBullets1;

        for (int i = 0; i < numBullets1; i++)
        {
            float angle1 = i * angleStep1;
            float radian1 = angle1 * Mathf.Deg2Rad;
            Vector3 direction1 = new Vector3(Mathf.Cos(radian1), Mathf.Sin(radian1), 0);

            ShootProjectile(transform.position, direction1, 1);
        }
    }

    private void Pattern2()
    {

        int numBullets2 = 12;
        float angleStep2 = 360.0f / numBullets2;

        for (int i = 0; i < numBullets2; i++)
        {
            float angle2 = i * angleStep2;
            float radian2 = angle2 * Mathf.Deg2Rad;
            Vector3 direction2 = new Vector3(Mathf.Cos(radian2), Mathf.Sin(radian2), 0);

            ShootProjectile(transform.position, direction2, 1);
        }


    }

    private IEnumerator Pattern3()
    {
        int numBullets = 5;
        float interval = 1.0f;

        for (int i = 0; i < numBullets; i++)
        {
            Vector3 playerDirection = (PlayerPosition() - transform.position).normalized;
            ShootProjectile(transform.position, playerDirection, 1);
            yield return new WaitForSeconds(interval);
        }
    }

    private void Pattern4()
    {
        int numBullets3 = 10;
        float angleStep3 = 360.0f / numBullets3;
        float radius = 2.0f;

        for (int i = 0; i < numBullets3; i++)
        {
            float angle3 = i * angleStep3;
            float radian3 = angle3 * Mathf.Deg2Rad;
            float x = radius * Mathf.Cos(radian3);
            float y = radius * Mathf.Sin(radian3);

            Vector3 direction3 = new Vector3(x, y, 0).normalized;

            ShootProjectile(transform.position, direction3, 1);
        }
    }

    private IEnumerator Pattern5()
    {
        _move = false;
        int numBullets = 7;
        float interval = 0.2f;

        for (int i = 0; i < numBullets; i++)
        {
            Vector3 playerDirection = (PlayerPosition() - transform.position).normalized;
            ShootProjectile(transform.position, playerDirection, 1);
            yield return new WaitForSeconds(interval);
        }
        _move = true;
    }

    private void Pattern6()
    {
        int numBullets4 = 15;
        float angleStep4 = 360.0f / numBullets4;

        for (int i = 0; i < numBullets4; i++)
        {
            float angle1 = i * angleStep4;
            float radian1 = angle1 * Mathf.Deg2Rad;
            Vector3 direction4 = new Vector3(Mathf.Cos(radian1), Mathf.Sin(radian1), 0);
            ShootProjectile(transform.position, direction4, 2);
        }
    }

    private Vector3 PlayerPosition()
    {

        return GameManager.Instance.PlayerCharacter.transform.position;
    }

    private void OnDestroy()
    {
        GameManager.Instance.StageClear();
    }
}
