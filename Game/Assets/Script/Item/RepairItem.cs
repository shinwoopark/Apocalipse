using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairItem : BaseItem
{
    public override void OnGetItem(GameManager gameManager)
    {
        base.OnGetItem(gameManager);
        PlayerHPSystem system = gameManager.PlayerCharacter.GetComponent<PlayerHPSystem>();
        if (system != null)
        {
            system.Health += 1;
            Destroy(gameObject);
        }
    }
}
