using Utils;

public class Magnet : Item
{
    protected override void Effect()
    {
        SimpleSingleton<MediatorManager>.Instance.Notify(EMediatorType.GetMagnet);
    }
}