using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Bomb : MonoBehaviour
{
    public SoundManager SoundManager;
    public GameObject SoundManager_gb;

    public GameObject BombProjactile;
    public float ProjectileMoveSpeed;

    public GameObject BombEX;
    public Transform BulletPos;

    public SpriteRenderer SpriteRenderer;
    private float _a;
    private bool bready = false;
    private void Start()
    {
        SoundManager_gb = GameObject.Find("Managers");
        SoundManager = SoundManager_gb.GetComponent<SoundManager>();

        Invoke("Attack", 3);
    }

    void Update()
    {
        AttackReady();
        
    }
    private void AttackReady()
    {
        SpriteRenderer.color = new Color(255, 0, 0, _a);
        if (!bready)
        {
            _a += Time.deltaTime * 0.2f;
        }
    }
    private void Attack()
    {
        bready = true;
        _a = 0;
        GameObject instance = Instantiate(BombProjactile, BulletPos.position, Quaternion.Euler(0, 0, 0));
        Projectile projectile = instance.GetComponent<Projectile>();
        projectile.MoveSpeed = ProjectileMoveSpeed;
        projectile.SetDirection(Vector3.down.normalized);
    }
    private void Destroy()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BombProjectile"))
        {
            SoundManager.PlaySFX(15);
            BombEX.SetActive(true);
            Destroy(collision.gameObject);
            Invoke("Destroy", 0.35f);
        }
    }
}
