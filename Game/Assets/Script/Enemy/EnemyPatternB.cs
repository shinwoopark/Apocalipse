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
        if (Enemy.bFreeze == false)
        {
            if (!_isAttack)
                Move();
        }
    }

    IEnumerator Attack()
    {
        while (true)
        {

            if (Enemy.bFreeze)
            {
                yield return new WaitForSeconds(0.5f);
                continue;
            }
            yield return new WaitForSeconds(1f);

            GameObject player_gb = GameObject.Find("PlayerCharacter");
            PlayerCharacter character = player_gb.GetComponent<PlayerCharacter>();
            if (character is null)
            {
                break;
            }

            Vector3 playerPos = character.GetComponent<Transform>().position;
            Vector3 direction = playerPos - transform.position;
            direction.Normalize();

            var projectile = Instantiate(Projectile, transform.position, Quaternion.identity);
            projectile.GetComponent<Projectile>().SetDirection(direction);
            projectile.GetComponent<Projectile>().MoveSpeed = ProjectileMoveSpeed;
            _isAttack = true;
            if (GameInstance.instance.CurrentStageLevel == 3)
            {
                yield return new WaitForSeconds(1);
                projectile.GetComponent<Projectile>().SetDirection(direction);
                projectile.GetComponent<Projectile>().MoveSpeed = ProjectileMoveSpeed;
            }
           

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
