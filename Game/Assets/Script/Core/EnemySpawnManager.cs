using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : BaseManager
{
    public GameObject[] Enemys;
    public GameObject Meteor;
    public Transform[] EnemySpawnTransform;
    public Transform BossSpawnTransform;
    public float CoolDownTime = 3;
    private float _bossCoolDownTime = 3;
    public int MaxSpawnEnemyCount;

    private int _spawnCount = 0;
    private int _bossSpawnCount;

    private bool _bSpawnBoss = false;

    public GameObject[] Bosses;

    private void Start()
    {
        if (GameInstance.instance == null)
        {
            return;
        }
        if (GameInstance.instance.CurrentStageLevel == 1)
        {
            _bossSpawnCount = 15;
        }
        else if (GameInstance.instance.CurrentStageLevel == 2)
        {
            _bossSpawnCount = 20;
        }
        else if (GameInstance.instance.CurrentStageLevel == 3)
        {
            _bossSpawnCount = 30;
        }
    }

    public override void Init(GameManager gameManager)
    {
        base.Init(gameManager);
        StartCoroutine(SpawnEnemy());
        StartCoroutine(SpawnMeteor());
    }

    IEnumerator SpawnEnemy()
    {
        while (!_bSpawnBoss)
        {
            yield return new WaitForSeconds(CoolDownTime);

            if (_spawnCount >= _bossSpawnCount)
            {
                yield return new WaitForSeconds(_bossCoolDownTime);
                if (GameInstance.instance.CurrentStageLevel == 1)
                {
                    Instantiate(Bosses[0], new Vector3(BossSpawnTransform.position.x, BossSpawnTransform.position.y + 1, 0f), Quaternion.identity);
                    _bSpawnBoss = true;
                }
                else if (GameInstance.instance.CurrentStageLevel == 2)
                {
                    Instantiate(Bosses[1], new Vector3(BossSpawnTransform.position.x, BossSpawnTransform.position.y + 1, 0f), Quaternion.identity);
                    _bSpawnBoss = true;
                }
                else if (GameInstance.instance.CurrentStageLevel == 3)
                {
                    Instantiate(Bosses[2], new Vector3(BossSpawnTransform.position.x, BossSpawnTransform.position.y + 1, 0f), Quaternion.identity);
                    _bSpawnBoss = true;
                }
            }

            int spawnCount = Random.Range(1, EnemySpawnTransform.Length + 1);
            List<int> availablePositions = new List<int>(EnemySpawnTransform.Length);

            for (int i = 0; i < EnemySpawnTransform.Length; i++)
            {
                availablePositions.Add(i);
            }

            for (int i = 0; i < spawnCount; i++)
            {
                int randomEnemy = Random.Range(0, Enemys.Length);
                int randomPositionIndex = Random.Range(0, availablePositions.Count - 1);
                int randomPosition = availablePositions[randomPositionIndex];

                availablePositions.RemoveAt(randomPositionIndex);

                Instantiate(Enemys[randomEnemy], EnemySpawnTransform[randomPosition].position, Quaternion.identity);
            }
            _spawnCount += spawnCount;
        }
    }
    IEnumerator SpawnMeteor()
    {
        yield return new WaitForSeconds(3);
        while (!_bSpawnBoss)
        {           
            int randomPosition = Random.Range(0, 4);
            Instantiate(Meteor, EnemySpawnTransform[randomPosition].position, Quaternion.identity);
            if (GameInstance.instance.CurrentStageLevel == 3)
            {
                int CoolDownTime = Random.Range(2, 5);
                yield return new WaitForSeconds(CoolDownTime);
            }
            else
            {
                int CoolDownTime = Random.Range(3, 6);
                yield return new WaitForSeconds(CoolDownTime);
            }
            
        }
    }
}
