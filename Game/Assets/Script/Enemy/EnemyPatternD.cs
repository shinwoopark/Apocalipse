using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatternD : MonoBehaviour
{
    public SoundManager SoundManager;
    public GameObject SoundManager_gb;

    public Enemy Enemy;
    public float MoveSpeed;
    public GameObject Projectile;
    public float ProjectileMoveSpeed;

    private Vector3 _direction;

    private void Start()
    {
        SoundManager_gb = GameObject.Find("Managers");
        SoundManager = SoundManager_gb.GetComponent<SoundManager>();
    }
    void Update()
    {
        if (Enemy.bFreeze == false)
        {
            PlayerPosUpdate();
            MoveUpdate();
        }
        if (GameInstance.instance.CurrentStageLevel == 3)
        {
            MoveSpeed = 7;
        }
    }
    public void Attack()
    {
        SoundManager.PlaySFX(10);
        if (Enemy.bFreeze == false)
        {
            Vector3 position = transform.position;
            if(GameInstance.instance.CurrentStageLevel == 3)
            {
                for (int i = 0; i < 360; i += 40)
                {
                    float angle = i * Mathf.Deg2Rad;
                    Vector3 direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);

                    ShootProjectile(position, direction);
                }

                Destroy(gameObject);
                return;
            }
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
        GameObject player_gb = GameObject.Find("PlayerCharacter");
        PlayerCharacter character = player_gb.GetComponent<PlayerCharacter>();
        if (character is null)
        {
            return;
        }
        Vector3 playerPos = character.GetComponent<Transform>().position;
        Vector3 direction = playerPos - transform.position;
        direction.Normalize();
        _direction = direction;
    }
}
