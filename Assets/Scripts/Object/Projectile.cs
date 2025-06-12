using UnityEngine;

public class Projectile : Poolable
{
    private Transform target;
    [SerializeField] private float speed;
    private float damage;

    public void Init(Transform target, float damage)
    {
        this.target = target;
        this.damage = damage;
    }

    private void FixedUpdate()
    {
        if (target == null || target.gameObject.activeSelf == false)
        {
            PoolManager.Instance.Release(this);
            return;
        }
        Vector2 dir = (target.position - transform.position).normalized;
        transform.Translate(dir * speed * Time.fixedDeltaTime, Space.World);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, dir);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            Monster monster = collision.GetComponent<Monster>();

            monster?.TakeDamage(damage);

            PoolManager.Instance.Release(this);
        }
    }
}
