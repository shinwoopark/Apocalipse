using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairSkill : BaseSkill
{
    public SoundManager SoundManager;
    public GameObject SoundManager_gb;

    void Start()
    {
        SoundManager_gb = GameObject.Find("Managers");
        SoundManager = SoundManager_gb.GetComponent<SoundManager>();
    }

    public override void Activate()
    {
        base.Activate();

        PlayerHPSystem system = playerCharacter.GetComponent<PlayerHPSystem>();
        if (system != null)
        {
            system.Health += 1;
            SoundManager.PlaySFX(5);
            if (system.Health >= system.MaxHealth)
            {
                system.Health = system.MaxHealth;
            }
        }
    }
}
