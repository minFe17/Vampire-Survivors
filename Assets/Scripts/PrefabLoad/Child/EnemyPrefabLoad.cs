using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyPrefabLoad : PrefabLoadBase
{
    Dictionary<EEnemyType, GameObject> _enemyPrefaDict = new Dictionary<EEnemyType, GameObject>();

    string GetAddressableKey(EEnemyType type)
    {
        return $"{type}";
    }

    public override async Task LoadPrefab()
    {
        if (_addressableManager == null)
            Init();
        for (int i = 0; i < (int)EEnemyType.Max; i++)
        {
            string key = GetAddressableKey((EEnemyType)i);
            GameObject prefab = await _addressableManager.GetAddressableAsset<GameObject>(key);
            if (prefab != null && !_enemyPrefaDict.ContainsKey((EEnemyType)i))
                _enemyPrefaDict.Add((EEnemyType)i, prefab);
        }
    }

    public override GameObject GetPrefab<TEnum>(TEnum type)
    {
        EEnemyType key = (EEnemyType)(object)type;
        return _enemyPrefaDict[key];
    }
}