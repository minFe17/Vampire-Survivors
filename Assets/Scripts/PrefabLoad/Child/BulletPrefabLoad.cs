using System.Threading.Tasks;
using UnityEngine;

public class BulletPrefabLoad : PrefabLoadBase
{
    GameObject _enemyPrefab;
    public override async Task LoadPrefab()
    {
        if (_addressableManager == null)
            Init();
        _enemyPrefab = await _addressableManager.GetAddressableAsset<GameObject>("Bullet");
    }

    public override GameObject GetPrefab()
    {
        return _enemyPrefab;
    }
}
