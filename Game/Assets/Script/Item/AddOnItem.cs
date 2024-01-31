using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddOnItem : BaseItem
{
    public GameObject AddOn;
    public PlayerCharacter playerCharacter;

    public override void OnGetItem(GameManager gameManager)
    {
        base.OnGetItem(gameManager);
        playerCharacter = gameManager.PlayerCharacter.GetComponent<PlayerCharacter>();

        if (GameInstance.instance.CurrentAddOnLevel == 2)
        {
            GameManager.Instance.AddScore(30);
            Destroy(gameObject);
            return;
        }
        Transform spawnTransform = playerCharacter.AddOnTransform[GameInstance.instance.CurrentAddOnLevel];
        SpawnAddOn(AddOn, spawnTransform.position, spawnTransform);

        GameInstance.instance.CurrentAddOnLevel++;
        Destroy(gameObject);
    }
    public static void SpawnAddOn(GameObject AddOn, Vector3 position, Transform followTransform)
    {
        GameObject inst = Instantiate(AddOn, position, Quaternion.identity);
        inst.GetComponent<AddOn>().FollowTransform = followTransform;
    }
}
