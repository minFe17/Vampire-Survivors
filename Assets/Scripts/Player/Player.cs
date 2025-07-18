using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

public class Player : MonoBehaviour
{
    [SerializeField] int _maxHp;
    [SerializeField] float _speed;

    SpriteRenderer _spriteRenderer;
    Animator _animator;
    Vector2 _movePos;

    int _currentHp;

    public Vector2 MovePos { get => _movePos; }

    #region Unity LifeCycle
    void Start()
    {
        _currentHp = _maxHp;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();

        SimpleSingleton<GameManager>.Instance.Player = this;
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
        
    }

    void Die()
    {

    }

    public void TakeDamage(int damage)
    {
        _currentHp -= damage;
        SimpleSingleton<MediatorManager>.Instance.Notify(EMediatorType.TakeDamagePlayer, (float)_currentHp / _maxHp);
        if (_currentHp <= 0)
            Die();
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