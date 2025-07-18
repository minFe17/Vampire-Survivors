using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] int _damage;
    [SerializeField] float _speed;
    [SerializeField] float _lifeTime;

    void Start()
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
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
            collision.GetComponent<Enemy>().TakeDamage(_damage);
        Remove();
    }
}