using UnityEngine;
using Utils;

public class WeaponFactory : MonoBehaviour, IFactory
{
    [SerializeField] protected EBulletType _bulletType;

    void Awake()
    {
        Register();
    }

    #region Interface
    GameObject IFactory.Create()
    {
        return Instantiate(SimpleSingleton<PrefabManager>.Instance.GetPrefabLoad(EPrefabType.Bullet).GetPrefab(_bulletType));
    }

    public void Register()
    {
        MonoSingleton<ObjectPoolManager>.Instance.RegisterFactory(_bulletType, this);
    }
    #endregion
}