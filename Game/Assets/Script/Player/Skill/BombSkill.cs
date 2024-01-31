using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSkill : BaseSkill
{
    public override void Activate()
    {
        base.Activate();

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject obj in enemies)
        {
            if (obj != null)
            {
                if (obj.tag == "Boss")
                    return;

                Enemy enemy = obj.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.Dead();
                }
            }
        }
        GameObject[] enemieBullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
        foreach (GameObject obj in enemieBullets)
        {
            Destroy(obj);
        }
    }
}
