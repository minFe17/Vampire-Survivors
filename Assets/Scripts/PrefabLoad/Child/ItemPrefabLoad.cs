using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ItemPrefabLoad : PrefabLoadBase
{
    Dictionary<EItemType, GameObject> _itemPrefaDict = new Dictionary<EItemType, GameObject>();

    string GetAddressableKey(EItemType type)
    {
        return $"{type}";
    }

    public override async Task LoadPrefab()
    {
        if (_addressableManager == null)
            Init();
        for (int i = 0; i < (int)EItemType.Max; i++)
        {
            string key = GetAddressableKey((EItemType)i);
            GameObject prefab = await _addressableManager.GetAddressableAsset<GameObject>(key);
            if (prefab != null && !_itemPrefaDict.ContainsKey((EItemType)i))
                _itemPrefaDict.Add((EItemType)i, prefab);
        }
    }

    public override GameObject GetPrefab<TEnum>(TEnum type)
    {
        EItemType key = (EItemType)(object)type;
        return _itemPrefaDict[key];
    }
}