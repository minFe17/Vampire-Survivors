using UnityEngine;
using Utils;

public class Exp : MonoBehaviour
{
    [SerializeField] int _expAmount;

    float _speed = 3f;
    float _targetDistance = 3f;

    void Update()
    {
        CheckDistance();    
    }

    void CheckDistance()
    {
        Vector2 targetPos = SimpleSingleton<GameManager>.Instance.Player.transform.position;
        Vector2 currentPos = transform.position;

        float distance = Vector2.Distance(currentPos, targetPos);

        if(distance < _targetDistance )
        {
            Vector2 direction = (targetPos - currentPos).normalized;
            transform.position += (Vector3)(direction * _speed * Time.deltaTime);
        }
    }

    void GetExp()
    {
        SimpleSingleton<ExpManager>.Instance.AddExp(_expAmount);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
            GetExp();
    }
}