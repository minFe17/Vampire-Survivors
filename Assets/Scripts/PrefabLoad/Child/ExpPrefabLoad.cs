using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ExpPrefabLoad : PrefabLoadBase
{
    Dictionary<EExpType, GameObject> _expPrefabDict = new Dictionary<EExpType, GameObject>();

    string GetAddressableKey(EExpType type)
    {
        return $"{type}";
    }

    public override async Task LoadPrefab()
    {
        if (_addressableManager == null)
            Init();
        for (int i = 0; i < (int)EExpType.Max; i++)
        {
            string key = GetAddressableKey((EExpType)i);
            GameObject prefab = await _addressableManager.GetAddressableAsset<GameObject>(key);
            if (prefab != null && !_expPrefabDict.ContainsKey((EExpType)i))
                _expPrefabDict.Add((EExpType)i, prefab);
        }
    }

    public override GameObject GetPrefab<TEnum>(TEnum type)
    {
        EExpType key = (EExpType)(object)type;
        return _expPrefabDict[key];
    }
}