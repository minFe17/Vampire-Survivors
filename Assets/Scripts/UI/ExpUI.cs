using UnityEngine;
using UnityEngine.UI;
using Utils;

public class ExpUI : MonoBehaviour, IMediatorEvent
{
    [SerializeField] Text _expText;
    [SerializeField] Image _expImage;

    void Start()
    {
        SimpleSingleton<MediatorManager>.Instance.Register(EMediatorType.GetExp, this);
    }

    void IMediatorEvent.HandleEvent(object data)
    {
        int exp = (int)data;
        int needExp = SimpleSingleton<ExpManager>.Instance.NeedExp;

        _expText.text = string.Format("{0:D3} / {1:D3}", exp, needExp);
        _expImage.fillAmount = (float)exp / needExp;
    }
}