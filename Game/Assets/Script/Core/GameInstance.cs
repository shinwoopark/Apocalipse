using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInstance : MonoBehaviour
{
    public static GameInstance instance;

    public float GameStartTime = 0f;
    public int Score = 0;
    public int CurrentStageLevel = 1;

    public int CurrentPlayerWeaponLevel = 1;
    public int CurrentPlayerHp = 3;
    public float CurrentPlayerFuel = 100f;
    public int CurrentAddOnLevel = 0;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
        GameStartTime = Time.time;
    }
}
