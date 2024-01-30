using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BaseSkill : MonoBehaviour
{
    protected PlayerCharacter playerCharacter;
    public float CooldownTime;
    public float CurrentTime;
    public bool bIsCoolDown = false;

    public void Init(PlayerCharacter _playerCharacter)
    {
        playerCharacter = _playerCharacter;
    }

    public void Update()
    {
        if (bIsCoolDown)
        {
            CurrentTime -= Time.deltaTime;
            if (CurrentTime <= 0)
            {
                bIsCoolDown = false;
            }
        }
    }

    public bool IsAvailable()
    {
        Debug.Log("shooooot");
        return !bIsCoolDown;
    }

    public virtual void Activate()
    {
        Debug.Log("shoot");
        bIsCoolDown = true;
        CurrentTime = CooldownTime;
    }

    public void InitCoolDown()
    {
        bIsCoolDown = false;
        CurrentTime = 0;
    }
}
