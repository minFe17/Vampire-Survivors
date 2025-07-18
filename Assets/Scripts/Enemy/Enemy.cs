using UnityEngine;
using Utils;

public class Enemy : MonoBehaviour
{
    [SerializeField] EEnemyType _enemyType;
    [SerializeField] int _maxHp;
    [SerializeField] int _damage;
    [SerializeField] float _speed;

    Player _target;
    Animator _animator;
    SpriteRenderer _spriteRenderer;

    int _currentHp;
    float _hitTimer;
    bool _hitPlayer;

    void Start()
    {
        _target = SimpleSingleton<GameManager>.Instance.Player;
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Move();
        HitPlayer();
    }

    void Move()
    {
        Vector3 move = _target.transform.position - transform.position;
        if (move.x < 0)
            _spriteRenderer.flipX = true;
        else
            _spriteRenderer.flipX = false;
        transform.Translate(move.normalized * Time.deltaTime * _speed);
    }

    void HitPlayer()
    {
        if (!_hitPlayer)
            return;
        _hitTimer += Time.deltaTime;
        if (_hitTimer >= 1f)
        {
            _target.TakeDamage(_damage);
            _hitTimer = 0f;
        }
    }

    void Die()
    {
        SimpleSingleton<EnemyManager>.Instance.KillEnemy(this);
        MonoSingleton<ObjectPoolManager>.Instance.Pull(_enemyType, this.gameObject);
        SimpleSingleton<ExpManager>.Instance.CreateExp(transform.position);
    }

    public void TakeDamage(int damage)
    {
        _currentHp -= damage;
        if (_currentHp <= 0)
            Die();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _hitPlayer = true;
            _target.TakeDamage(_damage);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _hitPlayer = false;
            _hitTimer = 0f;
        }
    }
}