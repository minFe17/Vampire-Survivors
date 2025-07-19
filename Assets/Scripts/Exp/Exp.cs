using UnityEngine;
using Utils;

public class Exp : MonoBehaviour, IMediatorEvent
{
    [SerializeField] EExpType _expType;
    [SerializeField] int _expAmount;

    float _speed = 3f;
    float _targetDistance = 3f;

    bool _isGetMagnet;

    public EExpType ExpType { get => _expType; }
    public int ExpAmount { get => _expAmount; }

    void Start()
    {
        SimpleSingleton<MediatorManager>.Instance.Register(EMediatorType.GetMagnet, this);
    }

    void OnEnable()
    {
        _isGetMagnet = false;
        _speed = 3f;
    }

    void Update()
    {
        if(_isGetMagnet)
            Move();
        CheckDistance();    
    }

    void CheckDistance()
    {
        Vector2 targetPos = SimpleSingleton<GameManager>.Instance.Player.transform.position;
        Vector2 currentPos = transform.position;

        float distance = Vector2.Distance(currentPos, targetPos);

        if(distance < _targetDistance )
            Move();
    }

    void Move()
    {
        Vector2 targetPos = SimpleSingleton<GameManager>.Instance.Player.transform.position;
        Vector2 currentPos = transform.position;

        Vector2 direction = (targetPos - currentPos).normalized;
        transform.position += (Vector3)(direction * _speed * Time.deltaTime);
    }

    void GetExp()
    {
        SimpleSingleton<ExpManager>.Instance.AddExp(this);
        MonoSingleton<ObjectPoolManager>.Instance.Push(_expType, gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
            GetExp();
    }

    void IMediatorEvent.HandleEvent(object data)
    {
        _isGetMagnet = true;
        _speed *= 3f;
    }
}