using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public SoundManager SoundManager;
    public GameObject SoundManager_gb;

    public float Health;
    public float AttackDamage = 1f;
    bool bIsDead = false;
    public bool bFreeze = false;
    private float _freezingTime;
    private int _phase = 1;
    public SpriteRenderer SpriteRenderer;
    public Color Red;
    public GameObject ExplodeFX;

    void Start()
    {
        SoundManager_gb = GameObject.Find("Managers");
        SoundManager = SoundManager_gb.GetComponent<SoundManager>();
        Phase();
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

    public void Phase()
    {
        if (GameInstance.instance.CurrentStageLevel == 3)
        {
            _phase = 3;
            SpriteRenderer.color = Red;
            Health *= 2;
        }
    }

    public void Dead()
    {
        if (!bIsDead)
        {
            if (gameObject.layer == 13)
            {
                Instantiate(ExplodeFX, transform.position, Quaternion.identity);
                GameManager.Instance.ItemManager.SpawnRandomItem(transform.position);
                SoundManager.PlaySFX(12);
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

    public void DropItem()
    {
        Instantiate(ExplodeFX, transform.position, Quaternion.identity);
        GameManager.Instance.ItemManager.SpawnRandomItem(transform.position);
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

            if(_phase != 3)
            {
                GetComponentInChildren<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            }
            else if(_phase == 3)
            {
                GetComponentInChildren<SpriteRenderer>().color = Red;
            }

            yield return new WaitForSeconds(0.1f);

            flickCount++;
        }
    }
}
