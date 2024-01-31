using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadAnim : MonoBehaviour
{
    private void Start()
    {
        Invoke("Dead", 0.35f);
    }

    private void Dead()
    {
        Destroy(gameObject);
    }
}
