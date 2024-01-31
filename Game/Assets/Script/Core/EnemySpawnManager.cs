using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : BaseManager
{
    public GameObject[] Enemys;
    public Transform[] EnemySpawnTrasnform;
    public float CoolDownTime;
    public int MaxSpawnEnemyCount;

    public int BossSpawnCount = 10;
    private int _enemyCount;
    private int _choseEnemy;
    private int _choseEnemySpawnPos1, _choseEnemySpawnPos2, _choseEnemySpawnPos3, _choseEnemySpawnPos4;
    private int usedEnemySpawnPos;

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
            if (BossSpawnCount == 0)
            {
                
            }
            else
            {
                if (BossSpawnCount >= 4)
                {
                    _choseEnemySpawnPos1 = 0;
                    _enemyCount = Random.Range(1, 5);
                    for (int i = 0; i < _enemyCount; i++)
                    {
                        _choseEnemy = Random.Range(1, 5);
                        _choseEnemySpawnPos1 = Random.Range(1, 5);
                        SpawnEnemy(Enemys[_choseEnemy], EnemySpawnTrasnform[_choseEnemySpawnPos1]);
                        if (i > 0)
                        {
                            while(_choseEnemySpawnPos1 == usedEnemySpawnPos)
                            {
                                _choseEnemySpawnPos1 = Random.Range(1, 5);
                            }
                        }
                        usedEnemySpawnPos = _choseEnemySpawnPos1;
                    }
                }
                
            }

            
            yield return null;
        }
    }
    private void SpawnEnemy(GameObject enemy, Transform transform)
    {
        Instantiate(enemy, transform.position, Quaternion.identity);
    }
}
