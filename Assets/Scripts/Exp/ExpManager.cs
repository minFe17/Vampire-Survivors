using UnityEngine;
using Utils;

public class ExpManager : MonoBehaviour
{
    // �̱���
    int _exp;
    int _needExp = 100; // ������ �����??

    public int NeedExp { get => _needExp; }

    public void AddExp(int exp)
    {
        _exp += exp;
        SimpleSingleton<MediatorManager>.Instance.Notify(EMediatorType.GetExp, _exp);
    }
}