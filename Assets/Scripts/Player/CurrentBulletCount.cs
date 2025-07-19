using Utils;

public class CurrentBulletCount : IMediatorEvent
{
    Player _player;

    public void Init(Player player)
    {
        _player = player;
        SimpleSingleton<MediatorManager>.Instance.Register(EMediatorType.UpgradeBulletCount, this);
    }

    void IMediatorEvent.HandleEvent(object data)
    {
        _player.SetBulletCOunt((int)data);
    }
}