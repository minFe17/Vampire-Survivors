using UnityEngine;
using UnityEngine.UI;
using Utils;

public class KillEnemyCountUi : MonoBehaviour, IMediatorEvent
{
    [SerializeField] Text _killCountText;

    void Start()
    {
        SimpleSingleton<MediatorManager>.Instance.Register(EMediatorType.KillEnemy, this);
    }

    public void HandleEvent(object data = null)
    {
        int killCount = (int)data;
        _killCountText.text = string.Format("{0:D3}", killCount);
    }
}
