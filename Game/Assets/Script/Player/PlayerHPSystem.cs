using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerHPSystem : MonoBehaviour
{
    public PlayerCharacter PlayerCharacter;
    public int Health;
    public int MaxHealth;

    void Start()
    {
        Health = GameInstance.instance.CurrentPlayerHp;
    }
    public void InitHealth()
    {
        Health = MaxHealth;
        GameInstance.instance.CurrentPlayerHp = Health;
    }

    IEnumerator HitFlick()
    {
        int flickCount = 0;

        while (flickCount < 5)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.2f);

            yield return new WaitForSeconds(0.1f);

            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);

            yield return new WaitForSeconds(0.1f);

            flickCount++;
        }
    }

    private void Invincibility()
    {
        PlayerCharacter.SetInvincibility(true, 0.5f);
    }

    private void HpDown(int i)
    {
        if(!PlayerCharacter.bInvincibilityCheat)
            Health = Health - i;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Meteor")
            && !GameManager.Instance.PlayerCharacter.bInvincibility
            && !GameManager.Instance.bStageCleared)
        {
            HpDown(1);
            Invincibility();
            StartCoroutine(HitFlick());
            if (collision.gameObject.layer != 13)
            {
                Destroy(collision.gameObject);
            }

            if (Health <= 0)
            {
                GameManager.Instance.StartCoroutine(PlayerCharacter.DeadProcess());
            }
        }
        if (collision.gameObject.CompareTag("Enemy")
            && !GameManager.Instance.PlayerCharacter.bInvincibility
            && !GameManager.Instance.bStageCleared)
        {
            HpDown(1);
            Invincibility();
            StartCoroutine(HitFlick());
            if (collision.gameObject.layer != 13)
            {
                Destroy(collision.gameObject);
            }
            
            if (Health <= 0)
            {
                GameManager.Instance.StartCoroutine(PlayerCharacter.DeadProcess());
            }
        }
        if (collision.gameObject.CompareTag("EnemyBullet")
            && !GameManager.Instance.PlayerCharacter.bInvincibility
            && !GameManager.Instance.bStageCleared)
        {
            HpDown(1);
            Invincibility();
            StartCoroutine(HitFlick());

            Destroy(collision.gameObject);

            if (Health <= 0)
            {
                GameManager.Instance.StartCoroutine(PlayerCharacter.DeadProcess());
            }
        }
        if (collision.gameObject.CompareTag("Boss")
            && !GameManager.Instance.PlayerCharacter.bInvincibility
            && !GameManager.Instance.bStageCleared)
        {
            HpDown(1);
            Invincibility();
            StartCoroutine(HitFlick());

            Destroy(collision.gameObject);

            if (Health <= 0)
            {
                GameManager.Instance.StartCoroutine(PlayerCharacter.DeadProcess());
            }
        }
        if (collision.gameObject.CompareTag("EnemyBomb")
            && !GameManager.Instance.PlayerCharacter.bInvincibility
            && !GameManager.Instance.bStageCleared)
        {
            HpDown(1);
            Invincibility();
            StartCoroutine(HitFlick());

            if (Health <= 0)
            {
                GameManager.Instance.StartCoroutine(PlayerCharacter.DeadProcess());
            }
        }
        if (collision.gameObject.CompareTag("Item"))
        {
            if (Health > MaxHealth)
            {
                Health = MaxHealth;
            }
        }

        GameInstance.instance.CurrentPlayerHp = Health;
    }
}