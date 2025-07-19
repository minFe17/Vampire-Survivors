using System.Threading.Tasks;
using UnityEngine;

public class DataPrefabLoad : PrefabLoadBase
{
    TextAsset _skillData;
    string _name;

    public override void Init()
    {
        base.Init();
        _name = "SkillData";
    }

    public override async Task LoadPrefab()
    {
        if (_addressableManager == null)
            Init();
        _skillData = await _addressableManager.GetAddressableAsset<TextAsset>(_name);
    }

    public override T GetPrefab<T>()
    {
        if (typeof(T) == typeof(TextAsset))
            return (T)(object)_skillData;
        return default(T);
    }
}