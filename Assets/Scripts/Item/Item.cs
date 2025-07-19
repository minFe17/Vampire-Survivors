using UnityEngine;
using Utils;

public abstract class Item : MonoBehaviour, IMediatorEvent
{
    [SerializeField] protected EItemType _itemType;
    protected abstract void Effect();

    void Start()
    {
        SimpleSingleton<MediatorManager>.Instance.Register(EMediatorType.GameEnd, this);
    }

    void Remove()
    {
        MonoSingleton<ObjectPoolManager>.Instance.Push(_itemType, gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Effect();
            Remove();
        }
    }

    void IMediatorEvent.HandleEvent(object data)
    {
        MonoSingleton<ObjectPoolManager>.Instance.Push(_itemType, gameObject);
    }
}