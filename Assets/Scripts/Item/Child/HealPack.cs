using UnityEngine;
using Utils;

public class HealPack : Item
{
    [SerializeField] int _hpAmount;

    protected override void Effect()
    {
        SimpleSingleton<GameManager>.Instance.Player.AddHp(_hpAmount);
    }
}