using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BulletPrefabLoad : PrefabLoadBase
{
    Dictionary<EBulletType, GameObject> _bulletPrefaDict = new Dictionary<EBulletType, GameObject>();

    string GetAddressableKey(EBulletType type)
    {
        return $"{type}";
    }

    public override async Task LoadPrefab()
    {
        if (_addressableManager == null)
            Init();
        for (int i = 0; i < (int)EBulletType.Max; i++)
        {
            string key = GetAddressableKey((EBulletType)i);
            GameObject prefab = await _addressableManager.GetAddressableAsset<GameObject>(key);
            if (prefab != null && !_bulletPrefaDict.ContainsKey((EBulletType)i))
                _bulletPrefaDict.Add((EBulletType)i, prefab);
        }
    }

    public override GameObject GetPrefab<TEnum>(TEnum type)
    {
        EBulletType key = (EBulletType)(object)type;
        return _bulletPrefaDict[key];
    }
}
