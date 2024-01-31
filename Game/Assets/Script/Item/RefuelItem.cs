using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefuelItem : BaseItem
{
    public override void OnGetItem(GameManager gameManager)
    {
        base.OnGetItem(gameManager);
        PlayerFuelSystem system = gameManager.PlayerCharacter.GetComponent<PlayerFuelSystem>();
        if (system != null)
        {
            system.Fuel = system.MaxFuel;
            GameInstance.instance.CurrentPlayerFuel = system.Fuel;
            
            Destroy(gameObject);
        }
    }
}
