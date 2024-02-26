using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Enemy Enemy;
    public int BossNumber;
    private int _DropCount = 0;
    void Start()
    {
        
    }

    void Update()
    {
        switch (BossNumber)
        {
            case 1:
                if (Enemy.Health <= 50 && _DropCount == 0)
                {
                    Enemy.DropItem();
                    _DropCount++;
                }
                break;
            case 2:
                if (Enemy.Health <= 100 && _DropCount == 0)
                {
                    Enemy.DropItem();
                    _DropCount++;
                }
                break;
            case 3:
                if (Enemy.Health <= 200 && _DropCount == 0)
                {
                    Enemy.DropItem();
                    _DropCount++;
                }
                if (Enemy.Health <= 100 && _DropCount == 1)
                {
                    Enemy.DropItem();
                    _DropCount++;
                }
                break;
        }
    }
}
