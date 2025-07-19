using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using Utils;

public class SelectSkillUI : MonoBehaviour, IMediatorEvent
{
    [SerializeField] List<SkillPanel> _skillPanel;

    List<SkillData> _skillData = new List<SkillData>();

    public void Init()
    {
        SimpleSingleton<MediatorManager>.Instance.Register(EMediatorType.LevelUp, this);
    }

    void ShowSkillPanel()
    {
        gameObject.SetActive(true);
       _skillData = SimpleSingleton<SkillManager>.Instance.RandomSkills;

        if (_skillData == null || _skillData.Count == 0)
            return;

        Time.timeScale = 0f;
        SpriteAtlas iconAtlas = SimpleSingleton<PrefabManager>.Instance.GetPrefabLoad(EPrefabType.SkillAtlas).GetPrefab<SpriteAtlas>();

        for (int i = 0; i < _skillPanel.Count; i++)
        {
            if (i < _skillData.Count)
            {
                _skillPanel[i].gameObject.SetActive(true);
                _skillPanel[i].SkillIcon.sprite = iconAtlas.GetSprite(_skillData[i].SpriteName);
                _skillPanel[i].Name.text = _skillData[i].Name;
            }
            else
                _skillPanel[i].gameObject.SetActive(false);
        }
    }

    public void OnClickSelectSkill(int skillIndex)
    {
        Time.timeScale = 1f;
        SkillData selectedSkill = _skillData[skillIndex];
        ApplySkillEffect(selectedSkill);
        gameObject.SetActive(false);
    }

    void ApplySkillEffect(SkillData skill)
    {
        int skillLevel = SimpleSingleton<SkillManager>.Instance.GetSkillLevel(skill.SkillType);
        switch (skill.SkillType)
        {
            case ESkillType.Sickle:
                SimpleSingleton<MediatorManager>.Instance.Notify(EMediatorType.CreateSickle, skill.SkillLevelData[skillLevel].EffectValue);
                break;
            case ESkillType.BulletDamage:
                SimpleSingleton<MediatorManager>.Instance.Notify(EMediatorType.UpgradeBullet, skill.SkillLevelData[skillLevel].EffectValue);
                break;
            case ESkillType.HpUp:
                SimpleSingleton<MediatorManager>.Instance.Notify(EMediatorType.UpgradeHp, skill.SkillLevelData[skillLevel].EffectValue);
                break;
            case ESkillType.BulletCount:
                SimpleSingleton<MediatorManager>.Instance.Notify(EMediatorType.UpgradeBulletCount, skill.SkillLevelData[skillLevel].EffectValue);
                break;
        }
        SimpleSingleton<SkillManager>.Instance.AddSkillLevel(skill.SkillType);
    }

    void IMediatorEvent.HandleEvent(object data)
    {
        ShowSkillPanel();
    }
}