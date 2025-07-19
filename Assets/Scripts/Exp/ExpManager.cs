using System.Collections.Generic;
using UnityEngine;
using Utils;

public class ExpManager
{
    // ╫л╠шео
    List<Exp> _currentExpList = new List<Exp>();
    int _exp;
    int _needExp = 100;

    public int NeedExp { get => _needExp; }

    public void CreateExp(Vector3 pos)
    {
        int randomExp = Random.Range(0, (int)EExpType.Max);
        GameObject temp = MonoSingleton<ObjectPoolManager>.Instance.Pull((EExpType)randomExp);
        temp.transform.position = pos;
        _currentExpList.Add(temp.GetComponent<Exp>());
    }

    public void AddExp(Exp exp)
    {
        _exp += exp.ExpAmount;
        SimpleSingleton<MediatorManager>.Instance.Notify(EMediatorType.GetExp, _exp);

        if(_exp >= _needExp)
            LevelUp();
        _currentExpList.Remove(exp);
    }

    public void LevelUp()
    {
        _exp -= _needExp;
        _needExp = _needExp * 2;
        SimpleSingleton<MediatorManager>.Instance.Notify(EMediatorType.GetExp, _exp);
        SimpleSingleton<MediatorManager>.Instance.Notify(EMediatorType.LevelUp);
    }

    public void EndGame()
    {
        for(int i=0; i<_currentExpList.Count; i++)
            MonoSingleton<ObjectPoolManager>.Instance.Push(_currentExpList[i].ExpType, _currentExpList[i].gameObject);
        _currentExpList.Clear();
        _exp = 0;
        _needExp = 100;
    }
}