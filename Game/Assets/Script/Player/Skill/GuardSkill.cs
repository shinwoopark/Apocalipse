using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GuardSkill : BaseSkill
{
    public GameObject GuardPos_gb;
    public GuardPos GuardPos;

    private void Start()
    {
        GuardPos_gb = GameObject.Find("GuardPos");
        GuardPos = GuardPos_gb.GetComponent<GuardPos>();
    }
    public override void Activate()
    {
        base.Activate();
        GuardPos.ActiveSkill();
    }
}
