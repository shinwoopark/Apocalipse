using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardBullet : MonoBehaviour
{
    public GameObject Player_gb;
    public Transform Player_tr;
    private void Awake()
    {
        Player_gb = GameObject.Find("PlayerCharacter");
        Player_tr = Player_gb.GetComponent<Transform>();
    }
    void FixedUpdate()
    {
        MoveUpdate();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Enemy":
                Destroy(collision.gameObject);
                Destroy(gameObject);
                break;
            case "Boss":
                Destroy(gameObject);
                break;
            case "EnemyBullet":
                Destroy(collision.gameObject);
                Destroy(gameObject);
                break;
            case "Meteor":
                Destroy(collision.gameObject);
                Destroy(gameObject);
                break;
        }
    }
    private void MoveUpdate()
    {
        transform.RotateAround(Player_tr.position, Vector3.back, 2);
    }
}
