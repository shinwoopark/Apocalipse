using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairSkill : BaseSkill
{
    public override void Activate()
    {
        base.Activate();

        PlayerHPSystem system = playerCharacter.GetComponent<PlayerHPSystem>();
        if (system != null)
        {
            system.Health += 1;

            if (system.Health >= system.MaxHealth)
            {
                system.Health = system.MaxHealth;
            }
        }
    }
}
