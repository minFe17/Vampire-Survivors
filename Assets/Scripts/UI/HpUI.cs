using UnityEngine;
using UnityEngine.UI;
using Utils;

public class HpUI : MonoBehaviour, IMediatorEvent
{
    [SerializeField] Image _hpBar;
    void Start()
    {
        SimpleSingleton<MediatorManager>.Instance.Register(EMediatorType.TakeDamagePlayer, this);
    }

    public void HandleEvent(object data = null)
    {
        float hpAmount = (float)data;
        _hpBar.fillAmount = hpAmount;
    }
}