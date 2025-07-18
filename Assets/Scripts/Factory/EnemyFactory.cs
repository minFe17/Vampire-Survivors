using UnityEngine;
using Utils;

public class EnemyFactory : MonoBehaviour, IFactory
{
    [SerializeField] protected EEnemyType _enemyType;

    void Start()
    {
        Register();
    }

    #region Interface
    GameObject IFactory.Create()
    {
        return Instantiate(SimpleSingleton<PrefabManager>.Instance.GetPrefabLoad(EPrefabType.Enemy).GetPrefab(_enemyType));
    }

    public void Register()
    {
        MonoSingleton<ObjectPoolManager>.Instance.RegisterFactory(_enemyType, this);
    }
    #endregion
}