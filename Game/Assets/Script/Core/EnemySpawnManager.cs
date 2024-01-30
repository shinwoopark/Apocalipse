using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : BaseManager
{
    public GameObject[] Enemys;
    public Transform[] EnemySpawnTrasnform;
    public float CoolDownTime;
    public int MaxSpawnEnemyCount;

    private int _spawnCount = 0;
    public int BossSpawnCount = 10;

    private bool _bspawnBoss = false;

    public GameObject[] Bosses;

    public override void Init(GameManager gameManager)
    {
        base.Init(gameManager);
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (!_bspawnBoss)
        {
            yield return null;
        }
    }
}
