using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

public class Player : MonoBehaviour, IMediatorEvent
{
    [SerializeField] int _maxHp;
    [SerializeField] float _speed;

    CurrentBulletCount _bulletCount = new CurrentBulletCount();
    SpriteRenderer _spriteRenderer;
    Animator _animator;
    Vector2 _movePos;

    int _currentHp;
    int _currentBulletCount = 1;

    public Vector2 MovePos { get => _movePos; }

    #region Unity LifeCycle
    void Start()
    {
        _currentHp = _maxHp;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _bulletCount.Init(this);

        SimpleSingleton<GameManager>.Instance.Player = this;
        SimpleSingleton<MediatorManager>.Instance.Register(EMediatorType.UpgradeHp, this);
        StartCoroutine(AttackRoutine());
    }

    void FixedUpdate()
    {
        Move();
    }
    #endregion

    void Move()
    {
        transform.Translate(_movePos.normalized * _speed * Time.deltaTime);
    }

    void Turn()
    {
        if (_movePos.x < 0)
            _spriteRenderer.flipX = true;
        else
            _spriteRenderer.flipX = false;
    }

    void Attack(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;

        float spreadAngle = 15f;
        int count = _currentBulletCount;

        for (int i = 0; i < count; i++)
        {
            GameObject bullet = MonoSingleton<ObjectPoolManager>.Instance.Pull(EBulletType.Bullet);
            bullet.transform.position = transform.position;

            float angleOffset = (i - (count - 1) / 2f) * spreadAngle;
            Quaternion rotation = Quaternion.AngleAxis(angleOffset, Vector3.forward);
            Vector3 spreadDirection = rotation * direction;

            bullet.transform.rotation = Quaternion.FromToRotation(Vector3.up, spreadDirection);
        }
    }

    void Die()
    {
        SimpleSingleton<MediatorManager>.Instance.Notify(EMediatorType.GameEnd, "FAIL...");
    }

    public void TakeDamage(int damage)
    {
        _currentHp -= damage;
        SimpleSingleton<MediatorManager>.Instance.Notify(EMediatorType.TakeDamagePlayer, (float)_currentHp / _maxHp);
        if (_currentHp <= 0)
            Die();
    }

    public void AddHp(int value)
    {
        _currentHp += value;
        SimpleSingleton<MediatorManager>.Instance.Notify(EMediatorType.TakeDamagePlayer, (float)_currentHp / _maxHp);
    }

    public void SetBulletCOunt(int bulletCOunt)
    {
        _currentBulletCount = bulletCOunt;
    }

    #region Unity InputSystem
    void OnMove(InputValue value)
    {
        _movePos = value.Get<Vector2>().normalized;

        bool isMoving = _movePos.sqrMagnitude > 0.01f;
        _animator.SetBool("isMove", isMoving);

        if (Mathf.Abs(_movePos.x) > 0.01f)
            Turn();
    }
    #endregion

    void IMediatorEvent.HandleEvent(object data)
    {
        _maxHp = (int)data;
    }

    #region Coroutine
    IEnumerator AttackRoutine()
    {
        while (true)
        {
            Transform target = SimpleSingleton<EnemyManager>.Instance.GetClosestEnemy(transform.position);
            if (target != null)
                Attack(target);
            yield return new WaitForSeconds(0.5f);
        }
    }
    #endregion
}