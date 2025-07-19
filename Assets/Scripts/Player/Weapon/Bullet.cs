using UnityEngine;
using Utils;

public class Bullet : MonoBehaviour, IMediatorEvent
{
    [SerializeField] int _damage;
    [SerializeField] float _speed;
    [SerializeField] float _lifeTime;

    int _currentDamage;

    void Start()
    {
        if( _currentDamage == 0 )
            _currentDamage = _damage;
        SimpleSingleton<MediatorManager>.Instance.Register(EMediatorType.UpgradeBullet, this);
        SimpleSingleton<MediatorManager>.Instance.Register(EMediatorType.GameEnd, this);
    }

    void OnEnable()
    {
        Invoke("Remove", _lifeTime);
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
    }

    void Remove()
    {
        CancelInvoke("Remove");
        MonoSingleton<ObjectPoolManager>.Instance.Push(EBulletType.Bullet, gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
            collision.GetComponent<Enemy>().TakeDamage(_currentDamage);
        Remove();
    }

    void IMediatorEvent.HandleEvent(object data)
    {
        if (data is int damage)
            _currentDamage = damage;
        else
        {
            _currentDamage = _damage;
            Remove();
        }
    }
}