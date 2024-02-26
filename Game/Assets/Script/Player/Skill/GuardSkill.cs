using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GuardSkill : BaseSkill
{
    public SoundManager SoundManager;
    public GameObject SoundManager_gb;

    public GameObject GuardPos_gb;
    public GuardPos GuardPos;

    private void Start()
    {
        SoundManager_gb = GameObject.Find("Managers");
        SoundManager = SoundManager_gb.GetComponent<SoundManager>();
        GuardPos_gb = GameObject.Find("GuardPos");
        GuardPos = GuardPos_gb.GetComponent<GuardPos>();
    }
    public override void Activate()
    {
        base.Activate();
        GameObject[] guardBullets = GameObject.FindGameObjectsWithTag("GuardBullet");
        foreach (GameObject obj in guardBullets)
        {
            Destroy(obj);
        }
        GuardPos.ActiveSkill();
        SoundManager.PlaySFX(4);
    }
}
