using UnityEngine;

[System.Serializable]
public class SkillLevelData
{
    [SerializeField] int _level;
    [SerializeField] int _effectValue;

    public int Level { get => _level; }
    public int EffectValue { get => _effectValue; }
}