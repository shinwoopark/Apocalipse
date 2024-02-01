using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilityItem : BaseItem
{
    public override void OnGetItem(GameManager gameManager)
    {
        gameManager.PlayerCharacter.GetComponent<PlayerCharacter>().SetInvincibility(true, 3);
        base.OnGetItem(gameManager);
        Destroy(gameObject);
    }
}
