using UnityEngine;
using Utils;

public class TimeManager : MonoBehaviour
{
    // 싱글턴
    float _timer;
    int _previousTime;

    public void Init()
    {

    }
    
    void Update()
    {
        _timer += Time.deltaTime;
        int currentTime = (int)_timer;

        if (currentTime != _previousTime)
        {
            _previousTime = currentTime;
            SimpleSingleton<MediatorManager>.Instance.Notify(EMediatorType.TimeTick, currentTime);
            SimpleSingleton<MediatorManager>.Instance.Notify(EMediatorType.SpawnEnemy);
        }
    }
}