using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

public class SkillManager : IMediatorEvent
{
    // ╫л╠шео
    Dictionary<ESkillType, SkillData> _skillDict = new Dictionary<ESkillType, SkillData>();
    Dictionary<ESkillType, int> _skillLevelDict = new Dictionary<ESkillType, int>();
    HashSet<ESkillType> _selectSkillHash = new HashSet<ESkillType>();

    public List<SkillData> RandomSkills
    {
        get
        {
            bool allMaxLevel = _skillDict.Keys.All(skill => _skillLevelDict.ContainsKey(skill) && _skillLevelDict[skill] >= 3);

            if (allMaxLevel)
                return new List<SkillData>();

            _selectSkillHash.Clear();

            List<SkillData> results = new List<SkillData>();
            int tryCount = 0;

            while (results.Count < 3 && tryCount < 10)
            {
                SkillData skill = RandomSkill();
                if (skill != null)
                    results.Add(skill);

                tryCount++;
            }
            return results;
        }
    }

    public void Init()
    {
        List<SkillData> skillData = SimpleSingleton<SkillDataList>.Instance.SkillData;

        for (int i = 0; i < skillData.Count; i++)
            _skillDict[skillData[i].SkillType] = skillData[i];
        SimpleSingleton<MediatorManager>.Instance.Register(EMediatorType.GameEnd, this);
    }

    SkillData RandomSkill()
    {
        List<SkillData> skillData = SimpleSingleton<SkillDataList>.Instance.SkillData;

        List<SkillData> skills = skillData
        .Where(s =>
            !_selectSkillHash.Contains(s.SkillType) &&
            (!SimpleSingleton<SkillManager>.Instance.GetSkillLevel(s.SkillType).Equals(3))
        )
        .ToList();

        if (skills.Count == 0)
            return null;

        SkillData selectSkill = skills[Random.Range(0, skills.Count)];
        _selectSkillHash.Add(selectSkill.SkillType);
        return selectSkill;
    }

    public int GetSkillLevel(ESkillType skilltype)
    {
        _skillLevelDict.TryGetValue(skilltype, out int level);
        return level;
    }

    public void AddSkillLevel(ESkillType skillType)
    {
        if ((_skillLevelDict.ContainsKey(skillType)))
            _skillLevelDict[skillType] += 1;
        else
            _skillLevelDict[skillType] = 1;
    }

    void IMediatorEvent.HandleEvent(object data)
    {
        _skillLevelDict.Clear();
    }
}