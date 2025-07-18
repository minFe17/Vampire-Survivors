using UnityEngine;
using UnityEngine.UI;
using Utils;

public class TimeUI : MonoBehaviour, IMediatorEvent
{
    [SerializeField] Text _timeText;

    void Start()
    {
        SimpleSingleton<MediatorManager>.Instance.Register(EMediatorType.TimeTick, this);
    }

    public void HandleEvent(object data = null)
    {
        int second = (int)data;
        _timeText.text = string.Format("{0:D2}:{1:D2}", second / 60, second % 60);
    }
}