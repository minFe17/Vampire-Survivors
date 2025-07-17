using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float _speed;

    SpriteRenderer _spriteRenderer;
    Animator _animator;
    Vector2 _movePos;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.Translate(_movePos * Time.deltaTime * _speed);
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
        _movePos = value.Get<Vector2>();
        if (_movePos != null)
            _animator.SetBool("isMove", true);
        else
            _animator.SetBool("isMove", false);
        if (_movePos.x != 0)
            Turn();
    }
    #endregion
}