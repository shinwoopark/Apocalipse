using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Health = 3f;
    public float AttackDamage = 1f;
    bool bIsDead = false;
    public bool Freeze = false;
    public bool bMustSpawnItem = false;
    private float _freezingTime;

    public GameObject ExplodeFX;

    void Start()
    {

    }

    void Update()
    {
        if (Freeze)
        {
            _freezingTime -= Time.deltaTime;
            if (_freezingTime <= 0)
            {
                _freezingTime = 0;
                Freeze = false;
            }
        }
    }

    public void Dead()
    {
        if (!bIsDead)
        {
            GameManager.Instance.EnemDies();

            if (gameObject.tag == "Boss")
            {
                GameManager.Instance.ItemManager.SpawnRandomItem(transform.position);
            }
            if (!bMustSpawnItem)
                GameManager.Instance.ItemManager.SpawnRandomItem(0, 3, transform.position);
            else
                GameManager.Instance.ItemManager.SpawnRandomItem(transform.position);

            Instantiate(ExplodeFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
            bIsDead = true;


        }
    }

    public void Freezing()
    {
        _freezingTime = 3;
        Freeze = true;
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
