using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossC : MonoBehaviour
{
    public GameObject Projectile_gb, Projectile2_gb, Projectile3_gb, Projectile4_gb;
    public Transform BulletPos1, BulletPos2;
    public float MoveSpeed = 2.0f;
    public float MoveDistance = 5.0f;

    private int _currentPatternIndex = 0;
    private int _basicAttackPatternIndex = 0;
    private float _attackCooltime = 1;
    private bool _movingRight = true;
    void Start()
    {
        Invoke("BasicAttack", 1);
        Invoke("NextPattern", 1.5f);
    }
    void Update()
    {
        Moving();
    }
    private void Moving()
    {
        if (transform.position.y >= 4)
        {
            transform.position += new Vector3(0, -1, 0) * MoveSpeed * Time.deltaTime;
        }
        else
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
    public void ShootProjectile(Vector3 position, Vector3 direction, int Bullet, float projectileMoveSpeed)    //ÃÑ¾Ë ¹ß»ç
    {
        if (Bullet == 3)
        {
            GameObject instance = Instantiate(Projectile3_gb, position, Quaternion.identity);
            Projectile projectile = instance.GetComponent<Projectile>();

            if (projectile != null)
            {
                projectile.MoveSpeed = projectileMoveSpeed;
                projectile.SetDirection(direction.normalized);
            }
        }
        else if (Bullet == 2)
        {
            GameObject instance = Instantiate(Projectile2_gb, position, Quaternion.identity);
            Projectile projectile = instance.GetComponent<Projectile>();

            if (projectile != null)
            {
                projectile.MoveSpeed = projectileMoveSpeed;
                projectile.SetDirection(direction.normalized);
            }
        }
        else
        {
            GameObject instance = Instantiate(Projectile_gb, position, Quaternion.identity);
            Projectile projectile = instance.GetComponent<Projectile>();

            if (projectile != null)
            {
                projectile.MoveSpeed = projectileMoveSpeed;
                projectile.SetDirection(direction.normalized);
            }
        }
    }
    private void BasicAttack()
    {
        Invoke("BasicAttack", 1);
        _basicAttackPatternIndex = Random.Range(1, 4);
        switch (_basicAttackPatternIndex)
        {
            case 1:
                Attack1();
                break;
            case 2:
                Attack2();
                break;
            case 3:
                Attack3();
                break;
        }
    }
    private void Attack1()
    {
        Vector3 playerDirection1 = (PlayerPosition() - BulletPos1.position).normalized;
        ShootProjectile(BulletPos1.position, playerDirection1, 1, 10);
        Vector3 playerDirection2 = (PlayerPosition() - BulletPos2.position).normalized;
        ShootProjectile(BulletPos2.position, playerDirection2, 1, 10);
    }
    private void Attack2()
    {
        Vector3 playerDirection1 = (PlayerPosition() - BulletPos1.position).normalized;
        ShootProjectile(BulletPos1.position, playerDirection1, 1, 2.5f);
        Vector3 playerDirection2 = (PlayerPosition() - BulletPos2.position).normalized;
        ShootProjectile(BulletPos2.position, playerDirection2, 1, 2.5f);
    }
    private void Attack3()
    {
        int numBullets = 6;
        float angleStep = 360.0f / numBullets;

        for (int i = 0; i < numBullets; i++)
        {
            float angle1 = i * angleStep;
            float radian1 = angle1 * Mathf.Deg2Rad;
            Vector3 playerDirection1 = new Vector3(Mathf.Cos(radian1), Mathf.Sin(radian1), 0);
            ShootProjectile(BulletPos1.position, playerDirection1, 2, 5);
            Vector3 playerDirection2 = new Vector3(Mathf.Cos(radian1), Mathf.Sin(radian1), 0);
            ShootProjectile(BulletPos2.position, playerDirection2, 2, 5);
        }
    }
    private void NextPattern()
    {
        _currentPatternIndex = Random.Range(1, 5);
        switch (_currentPatternIndex)
        {
            case 1:
                StartCoroutine(Pattern1());
                break;
            case 2:
                StartCoroutine(Pattern2());
                break;
            case 3:
                StartCoroutine(Pattern3());
                break;
            case 4:
                Pattern4();
                break;
        }
    }
    private IEnumerator Pattern1()
    {
        int numBullets = 30;
        float interval = 0.1f;

        for (int i = 0; i < numBullets; i++)
        {
            Vector3 playerDirection = (PlayerPosition() - transform.position).normalized;
            ShootProjectile(transform.position, playerDirection, 1, 5);
            yield return new WaitForSeconds(interval);
            if (i == numBullets - 1)
            {
                yield return new WaitForSeconds(_attackCooltime);
                NextPattern();
            }
        }
    }
    private IEnumerator Pattern2()
    {
        int numBullets = 15;
        float interval = 0.3f;
        float angleStep = 360.0f / numBullets;

        for (int i = 0; i < numBullets; i++)
        {
            float angle1 = i * angleStep;
            float radian1 = angle1 * Mathf.Deg2Rad;
            Vector3 direction4 = new Vector3(Mathf.Cos(radian1), Mathf.Sin(radian1), 0);
            ShootProjectile(transform.position, direction4, 2, 5);
            yield return new WaitForSeconds(interval);
            if (i == numBullets - 1)
            {
                yield return new WaitForSeconds(_attackCooltime);
                NextPattern();
            }
        }
    }
    private IEnumerator Pattern3()
    {
        int numBullets = 5;
        float interval = 0.5f;

        for (int i = 0; i < numBullets; i++)
        {
            Vector3 playerDirection = (PlayerPosition() - transform.position).normalized;
            ShootProjectile(transform.position, playerDirection, 3, 5);
            yield return new WaitForSeconds(interval);
            if (i == numBullets - 1)
            {
                yield return new WaitForSeconds(_attackCooltime);
                NextPattern();
            }
        }
    }
    private void Pattern4()
    {
        int numBullets = 15;
        float angleStep = 360.0f / numBullets;
        Invoke("NextPattern", 1);

        for (int i = 0; i < numBullets; i++)
        {
            float angle1 = i * angleStep;
            float radian1 = angle1 * Mathf.Deg2Rad;
            Vector3 direction4 = new Vector3(Mathf.Cos(radian1), Mathf.Sin(radian1), 0);
            ShootProjectile(transform.position, direction4, 2, 5);
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
