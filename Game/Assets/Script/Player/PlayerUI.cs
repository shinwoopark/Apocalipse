using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CoolDownText
{
    public EnumTypes.PlayerSkill Skill;
    public TextMeshProUGUI Text;
}

public class PlayerUI : MonoBehaviour
{
    public Image[] HealthImages = new Image[3];
    public Image RepairSkill;
    public Image BombSkill;
    public Slider FuelSlider;

    public TextMeshProUGUI SkillCooldownNoticeText;
    public List<CoolDownText> SkillCooldownTexts;

    private void Start()
    {
    }

    private void Update()
    {
        UpdateHealth();
        UpdateSkills();
        UpdateFuel();
    }

    private void UpdateHealth()
    {
        int health = GameManager.Instance.PlayerCharacter.GetComponent<PlayerHPSystem>().Health;   //Hp

        for (int i = 0; i < HealthImages.Length; i++)
        {
            HealthImages[i].gameObject.SetActive(i < health);
        }
    }

    private void UpdateSkills()
    {
        foreach (var item in SkillCooldownTexts)
        {
            bool isCoolDown = GameManager.Instance.PlayerCharacter.Skills[item.Skill].bIsCoolDown;
            float currentTime = GameManager.Instance.PlayerCharacter.Skills[item.Skill].CurrentTime;

            item.Text.gameObject.SetActive(isCoolDown);
            item.Text.text = $"{Mathf.RoundToInt(currentTime)}";
        }
    }

    private void UpdateFuel()
    {
        FuelSlider.GetComponent<Slider>().value = GameManager.Instance.PlayerCharacter.GetComponent<PlayerFuelSystem>().Fuel / 100f;
    }

    public void NoticeSkillCooldown(EnumTypes.PlayerSkill playerSkill)
    {
        StartCoroutine(NoticeText(playerSkill));
    }

    IEnumerator NoticeText(EnumTypes.PlayerSkill playerSkill)
    {
        SkillCooldownNoticeText.gameObject.SetActive(true);
        SkillCooldownNoticeText.text = $"{playerSkill.ToString()} Skill is Cooldown";

        yield return new WaitForSeconds(3);

        SkillCooldownNoticeText.gameObject.SetActive(false);
    }
}
