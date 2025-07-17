using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

public class Player : MonoBehaviour
{
    [SerializeField] float _speed;

    SpriteRenderer _spriteRenderer;
    Animator _animator;
    Rigidbody2D _rigidbody;
    Vector2 _movePos;

    public Vector2 MovePos { get => _movePos; }

    #region Unity LifeCycle
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();

        SimpleSingleton<GameManager>.Instance.Player = this;
    }

    void FixedUpdate()
    {
        Move();
    }
    #endregion

    void Move()
    {
        _rigidbody.linearVelocity = _movePos * _speed;
    }

    void Turn()
    {
        if (_movePos.x < 0)
            _spriteRenderer.flipX = true;
        else
            _spriteRenderer.flipX = false;  
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
}