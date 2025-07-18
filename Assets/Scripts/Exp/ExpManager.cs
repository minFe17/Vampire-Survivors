using UnityEngine;
using Utils;

public class ExpManager : MonoBehaviour
{
    // 싱글턴
    int _exp;
    int _needExp = 100; // 데이터 입출력??

    public int NeedExp { get => _needExp; }

    public void AddExp(int exp)
    {
        _exp += exp;
        SimpleSingleton<MediatorManager>.Instance.Notify(EMediatorType.GetExp, _exp);
    }
}