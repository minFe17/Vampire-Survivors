using System.Collections.Generic;
using UnityEngine;
using Utils;

public class SickleController : MonoBehaviour, IMediatorEvent
{
    [SerializeField] float _speed;

    List<GameObject> _currentSickle = new List<GameObject>();

    void Awake()
    {
        SimpleSingleton<MediatorManager>.Instance.Register(EMediatorType.CreateSickle, this);
    }

    void Update()
    {
        transform.Rotate(Vector3.back * _speed * Time.deltaTime);
    }

    void CreateSickle(int count)
    {
        int newCreateCount = count - _currentSickle.Count;

        for (int i = 0; i < newCreateCount; i++)
             _currentSickle.Add(MonoSingleton<ObjectPoolManager>.Instance.Pull(EBulletType.Sickle));

        for(int i=0; i<_currentSickle.Count; i++)
        {
            _currentSickle[i].transform.parent = gameObject.transform;
            float angle = (360f / count) * i;
            _currentSickle[i].transform.localRotation = Quaternion.Euler(0f, 0f, angle);
            _currentSickle[i].transform.localPosition = _currentSickle[i].transform.right * 1.5f;
        }
    }

    void IMediatorEvent.HandleEvent(object data)
    {
        int count = (int)data;
        CreateSickle(count);
    }
}