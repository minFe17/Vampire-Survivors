using System.Collections.Generic;
using System.Threading.Tasks;

public class PrefabManager
{
    // 싱글턴
    Dictionary<EPrefabType, PrefabLoadBase> _prefabDict;

    void SetDictionary()
    {
        _prefabDict = new Dictionary<EPrefabType, PrefabLoadBase>
        {
            {EPrefabType.Enemy, new EnemyPrefabLoad()}
        };
    }
    
    public async Task LoadPrefab()
    {
        SetDictionary();
        foreach (PrefabLoadBase prefabLoad in _prefabDict.Values)
            await prefabLoad.LoadPrefab();
    }

    public PrefabLoadBase GetPrefabLoad(EPrefabType key)
    {
        return _prefabDict[key];
    }
}