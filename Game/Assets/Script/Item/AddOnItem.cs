using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddOnItem : BaseItem
{
    public GameObject AddOn;
    public PlayerCharacter playerCharacter;
    private void Start()
    {

    }
    public override void OnGetItem(GameManager gameManager)
    {
        playerCharacter = gameManager.PlayerCharacter.GetComponent<PlayerCharacter>();

        if (gameManager != null && gameManager.PlayerCharacter)
        {
            if (GameInstance.instance.CurrentAddOnLevel == 2)
            {
                Destroy(gameObject);
                return;
            }
            Transform spawnTransform = playerCharacter.AddOnTransform[GameInstance.instance.CurrentAddOnLevel];
            SpawnAddOn(AddOn, spawnTransform.position, spawnTransform);

            GameInstance.instance.CurrentAddOnLevel++;
            Destroy(gameObject);
        }



    }
    public static void SpawnAddOn(GameObject AddOn, Vector3 position, Transform followTransform)
    {
        GameObject inst = Instantiate(AddOn, position, Quaternion.identity);
        inst.GetComponent<AddOn>().FollowTransform = followTransform;
    }
}
