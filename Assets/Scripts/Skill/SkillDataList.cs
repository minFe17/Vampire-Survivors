using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillDataList
{
    [SerializeField] List<SkillData> _skillData = new List<SkillData>();

    public List<SkillData> SkillData { get =>_skillData; }
}