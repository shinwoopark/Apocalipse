using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumTypes;
using static UnityEditor.Progress;

[System.Serializable]
public class Item
{
    public EnumTypes.ItemName Name;
    public GameObject Prefab;
}

public class BaseItem : MonoBehaviour
{
    protected void Update()
    {
        transform.Translate(new Vector3(0, -0.005f, 0f));
    }

    public virtual void OnGetItem(GameManager gameManager) { }
}

public class ItemManager : MonoBehaviour
{
    public List<Item> Items = new List<Item>();
    public void SpawnItem(EnumTypes.ItemName name, Vector3 position)
    {
        Item foundItem = Items.Find(item => item.Name == name);

        if (foundItem != null)
        {
            GameObject itemPrefab = foundItem.Prefab;
            GameObject inst = Instantiate(itemPrefab, position, Quaternion.identity);

            inst.GetComponent<Animator>().SetInteger("ItemIndex", (int)name);
        }
    }

    public void SpawnRandomItem(int min, int max, Vector3 position)
    {
        if (Random.Range(0, 3) == 0)
        {
            SpawnItem(EnumTypes.ItemName.Refuel, position);
            return;
        }

        if (Random.Range(min, max) == min)
        {
            int randomInt = Random.Range(0, 4);
            EnumTypes.ItemName itemName = (EnumTypes.ItemName)randomInt;
            SpawnItem(itemName, position);
        }
    }

    public void SpawnRandomItem(Vector3 position)
    {
        int randomInt = Random.Range(0, 4);
        EnumTypes.ItemName itemName = (EnumTypes.ItemName)randomInt;
        SpawnItem(itemName, position);
    }
}
