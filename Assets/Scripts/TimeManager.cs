using UnityEngine;
using Utils;

public class TimeManager : MonoBehaviour
{
    // 싱글턴
    float _timer;
    int _previousTime;
    float _itemBoxTimer;

    public void Init()
    {
        _timer = 0f;
        _previousTime = 0;
        _itemBoxTimer = 0f; ;
    }
    
    void Update()
    {
        _timer += Time.deltaTime;
        _itemBoxTimer += Time.deltaTime;

        int currentTime = (int)_timer;
        if (currentTime != _previousTime)
        {
            _previousTime = currentTime;
            SimpleSingleton<MediatorManager>.Instance.Notify(EMediatorType.TimeTick, currentTime);
            SimpleSingleton<MediatorManager>.Instance.Notify(EMediatorType.SpawnEnemy);
        }
        if (_itemBoxTimer >= 3f)
            SpawnItemBox();
    }

    void SpawnItemBox()
    {
        _itemBoxTimer = 0f;
        GameObject itemBox = MonoSingleton<ObjectPoolManager>.Instance.Pull(EItemType.ItemBox);
        Vector2 playerPos = SimpleSingleton<GameManager>.Instance.Player.transform.position;
        int randomPosX = Random.Range((int)playerPos.x - 16, (int)playerPos.x + 16);
        int randomPosY = Random.Range((int)playerPos.x - 10, (int)playerPos.x + 10);
        itemBox.transform.position = new Vector2(randomPosX, randomPosY);
    }
}