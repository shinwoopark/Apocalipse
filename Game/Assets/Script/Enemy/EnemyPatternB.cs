using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatternB : MonoBehaviour
{
    public Enemy Enemy;
    public float MoveSpeed;
    public float AttackStopTime;
    public float MoveTime;
    public GameObject Projectile;
    public float ProjectileMoveSpeed;

    private bool _isAttack = false;

    void Start()
    {
        StartCoroutine(Attack());
    }

    void Update()
    {
        if (Enemy.Freeze == false)
        {
            if (false == _isAttack)
                Move();
        }
    }

    IEnumerator Attack()
    {
        while (true)
        {

            if (Enemy.Freeze)
            {
                yield return new WaitForSeconds(0.5f);
                continue;
            }
            yield return new WaitForSeconds(1f);

            GameObject manager = GameObject.Find("Managers");
            PlayerCharacter character = manager.GetComponent<GameManager>().PlayerCharacter;
            if (character is null)
            {
                Debug.Log("Player is null");
                break;
            }

            Vector3 playerPos = character.GetComponent<Transform>().position;
            Vector3 direction = playerPos - transform.position;
            direction.Normalize();

            var projectile = Instantiate(Projectile, transform.position, Quaternion.identity);
            projectile.GetComponent<Projectile>().SetDirection(direction);
            projectile.GetComponent<Projectile>().MoveSpeed = ProjectileMoveSpeed;
            _isAttack = true;

            yield return new WaitForSeconds(AttackStopTime);

            _isAttack = false;

            yield return new WaitForSeconds(MoveTime);

        }
    }

    void Move()
    {
        if (!_isAttack)
        transform.position -= new Vector3(0f, MoveSpeed * Time.deltaTime, 0f);
    }
}
