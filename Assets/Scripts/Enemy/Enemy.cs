using UnityEngine;
using Utils;

public class Enemy : MonoBehaviour
{
    [SerializeField] float _speed;
    Transform _target;
    Animator _animator;
    SpriteRenderer _spriteRenderer;

    void Start()
    {
        _target = SimpleSingleton<GameManager>.Instance.Player.transform;
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector3 move = _target.position - transform.position;
        if (move.x < 0)
            _spriteRenderer.flipX = true;
        else
            _spriteRenderer.flipX = false;
        transform.Translate(move.normalized * Time.deltaTime * _speed);
    }
}
