using UnityEngine;
using Utils;

public class ExpFactory : MonoBehaviour, IFactory
{
    [SerializeField] protected EExpType _expType;

    void Start()
    {
        Register();
    }

    #region Interface
    GameObject IFactory.Create()
    {
        return Instantiate(SimpleSingleton<PrefabManager>.Instance.GetPrefabLoad(EPrefabType.Exp).GetPrefab(_expType));
    }

    public void Register()
    {
        MonoSingleton<ObjectPoolManager>.Instance.RegisterFactory(_expType, this);
    }
    #endregion
}