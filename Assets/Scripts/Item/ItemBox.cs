using UnityEngine;
using Utils;

public class ItemBox : MonoBehaviour, IMediatorEvent
{
    void Start()
    {
        SimpleSingleton<MediatorManager>.Instance.Register(EMediatorType.GameEnd, this);
    }

    void SpawnItem()
    {
        int randomIndex = Random.Range((int)EItemType.HealPack, (int)EItemType.Max);
        GameObject item = MonoSingleton<ObjectPoolManager>.Instance.Pull((EItemType)randomIndex);
        item.transform.position = this.transform.position;
        MonoSingleton<ObjectPoolManager>.Instance.Push(EItemType.ItemBox, gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
            SpawnItem();
    }

    void IMediatorEvent.HandleEvent(object data)
    {
        MonoSingleton<ObjectPoolManager>.Instance.Push(EItemType.ItemBox, gameObject);
    }
}