using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float Health = 3f;
    public float AttackDamage = 1f;
    bool bIsDead = false;
    public bool bFreeze = false;
    private float _freezingTime;

    public GameObject ExplodeFX;

    void Start()
    {

    }

    void Update()
    {
        if (bFreeze)
        {
            _freezingTime -= Time.deltaTime;
            if (_freezingTime <= 0)
            {
                _freezingTime = 0;
                bFreeze = false;
            }
        }
    }

    public void Dead()
    {
        if (!bIsDead)
        {
            if (gameObject.tag == "Boss")
            {
                Instantiate(ExplodeFX, transform.position, Quaternion.identity);
                GameManager.Instance.ItemManager.SpawnRandomItem(transform.position);
                Destroy(gameObject);
                bIsDead = true;
                return;
            }
            Instantiate(ExplodeFX, transform.position, Quaternion.identity);
            GameManager.Instance.EnemDies();
            GameManager.Instance.ItemManager.SpawnRandomItem(0, 3, transform.position);
            Destroy(gameObject);
            bIsDead = true;
        }
    }
    private void BossCHp()
    {
        int phase = 1;
        if (gameObject.name == "BossC")
        {
            if(phase == 1)
            {
                phase = 2;
            }
        }
    }
    public void Freezing()
    {
        _freezingTime = 3;
        bFreeze = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            Health -= 1f;

            if (Health <= 0f)
            {
                Dead();
            }

            StartCoroutine(HitFlick());
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            if (gameObject.layer != 10)
            {
                Destroy(gameObject);
            }
        }
    }
    IEnumerator HitFlick()
    {
        int flickCount = 0;

        while (flickCount < 1)
        {
            GetComponentInChildren<SpriteRenderer>().color = new Color(1, 0, 0, 0.5f);

            yield return new WaitForSeconds(0.1f);

            GetComponentInChildren<SpriteRenderer>().color = new Color(1, 1, 1, 1);

            yield return new WaitForSeconds(0.1f);

            flickCount++;
        }
    }
}
