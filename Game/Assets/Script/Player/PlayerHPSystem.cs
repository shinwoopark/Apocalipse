using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerHPSystem : MonoBehaviour
{
    public int Health;
    public int MaxHealth;

    void Start()
    {
        
    }

    public void InitHealth()
    {

    }

    IEnumerator HitFlick()
    {
        yield return new WaitForSeconds(Health);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")
            && !GameManager.Instance.PlayerCharacter.bInvincibility
            && !GameManager.Instance.bStageCleared)
        {

        }

        if (collision.gameObject.CompareTag("Item"))
        {

        }
    }
}
