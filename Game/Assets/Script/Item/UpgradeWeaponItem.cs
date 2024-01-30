using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeWeaponItem : BaseItem
{
    public override void OnGetItem(GameManager gameManager)
    {
        if (gameManager != null && gameManager.PlayerCharacter)
        {
            PlayerCharacter playerCharacter = gameManager.PlayerCharacter.GetComponent<PlayerCharacter>();

            int currentLevel = playerCharacter.CurrentWeaponLevel;
            int maxLevel = playerCharacter.MaxWeaponLevel;

            if (currentLevel >= maxLevel)
            {
                GameManager.Instance.AddScore(30);
                Destroy(gameObject);
                return;
            }

            playerCharacter.CurrentWeaponLevel = Mathf.Clamp(currentLevel + 1, 0, maxLevel);
            GameInstance.instance.CurrentPlayerWeaponLevel = playerCharacter.CurrentWeaponLevel;
            Destroy(gameObject);
        }
    }
}
