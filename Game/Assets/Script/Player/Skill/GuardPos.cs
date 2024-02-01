using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardPos : MonoBehaviour
{
    public PlayerCharacter PlayerCharacter;
    public GameObject GuardBullet_gb;

    public void ActiveSkill()
    {
        for (int i = 0; i < 360; i += 60)
        {
            float angle = i * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Cos(angle) + PlayerCharacter.transform.position.x, Mathf.Sin(angle) + PlayerCharacter.transform.position.y, 0);
            Instantiate(GuardBullet_gb, direction, Quaternion.identity, this.transform);
        }
    }
}
