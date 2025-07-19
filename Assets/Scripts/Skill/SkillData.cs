using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillData
{
    [SerializeField] string _name;
    [SerializeField] int _skillType;
    [SerializeField] string _spriteName;
    [SerializeField] List<SkillLevelData> _skillData;

    public string Name { get => _name; }
    public ESkillType SkillType { get => (ESkillType)_skillType; }
    public string SpriteName { get => _spriteName; }
    public List<SkillLevelData> SkillLevelData { get => _skillData; }
}