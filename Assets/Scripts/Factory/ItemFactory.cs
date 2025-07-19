using UnityEngine;
using Utils;

public class ItemFactory : MonoBehaviour, IFactory
{
    [SerializeField] protected EItemType _itemType;

    void Awake()
    {
        Register();
    }

    #region Interface
    GameObject IFactory.Create()
    {
        return Instantiate(SimpleSingleton<PrefabManager>.Instance.GetPrefabLoad(EPrefabType.Item).GetPrefab(_itemType));
    }

    public void Register()
    {
        MonoSingleton<ObjectPoolManager>.Instance.RegisterFactory(_itemType, this);
    }
    #endregion
}