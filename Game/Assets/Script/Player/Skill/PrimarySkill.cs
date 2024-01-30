using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//무기를 단계별로 강화시켜주고 총알의 발사 속도와 궤도를 관리한다

public class PrimarySkill : BaseSkill
{

    public float ProjectileMoveSpeed;
    public GameObject Projectile;

    private Weapon[] weapons;

    void Start()
    {
        CooldownTime = 0.2f;

        weapons = new Weapon[6];

        weapons[0] = new Level1Weapon();
        weapons[1] = new Level2Weapon();
        weapons[2] = new Level3Weapon();
        weapons[3] = new Level4Weapon();
        weapons[4] = new Level5Weapon();
        weapons[5] = new Level6Weapon();
    }

    public override void Activate()
    {
        base.Activate();
        weapons[GameInstance.instance.CurrentPlayerWeaponLevel].Activate(this, playerCharacter);
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
}

public interface Weapon
{
    void Activate(PrimarySkill primarySkill, GameManager gameManager);
}

public class Level1Weapon : Weapon
{
    public void Activate(PrimarySkill primarySkill, GameManager gameManager)
    {
        Vector3 position = gameManager.transform.position;
        primarySkill.ShootProjectile(position, Vector3.up);
    }
}

public class Level2Weapon : Weapon
{
    public void Activate(PrimarySkill primarySkill, GameManager gameManager)
    {
        Vector3 position = gameManager.transform.position;
        position.x -= 0.1f;

        for (int i = 0; i < 2; i++)
        {
            primarySkill.ShootProjectile(position, Vector3.up);
            position.x += 0.2f;
        }
    }
}

public class Level3Weapon : Weapon
{
    public void Activate(PrimarySkill primarySkill, GameManager gameManager)
    {
        Vector3 position = gameManager.transform.position;

        primarySkill.ShootProjectile(position, Vector3.up);
        primarySkill.ShootProjectile(position, new Vector3(0.3f, 1, 0));
        primarySkill.ShootProjectile(position, new Vector3(-0.3f, 1, 0));
    }
}

public class Level4Weapon : Weapon
{
    public void Activate(PrimarySkill primarySkill, GameManager gameManager)
    {
        Vector3 position = gameManager.transform.position;
        position.x -= 0.1f;

        for (int i = 0; i < 2; i++)
        {
            primarySkill.ShootProjectile(position, Vector3.up);
            position.x += 0.2f;
        }

        Vector3 position2 = gameManager.transform.position;
        primarySkill.ShootProjectile(position2, new Vector3(0.3f, 1, 0));
        primarySkill.ShootProjectile(position2, new Vector3(-0.3f, 1, 0));
    }
}

public class Level5Weapon : Weapon
{
    public void Activate(PrimarySkill primarySkill, GameManager gameManager)
    {
        Vector3 position = gameManager.transform.position;

        for (int i = 0; i < 180; i += 10)
        {
            float angle = i * Mathf.Deg2Rad;
            Debug.Log(angle);
            Vector3 direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);

            primarySkill.ShootProjectile(position, direction);
        }
    }
}
public class Level6Weapon : Weapon
{
    public void Activate(PrimarySkill primarySkill, GameManager gameManager)
    {
        Vector3 position = gameManager.transform.position;

        for (int i = 0; i < 360; i += 6)
        {
            float angle = i * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);

            primarySkill.ShootProjectile(position, direction);
        }
    }
}