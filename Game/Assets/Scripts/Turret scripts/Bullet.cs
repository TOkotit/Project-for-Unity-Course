using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{   
    [Header("Bullet Parameters")]
    [SerializeField] private float speed = 70f;
    private Transform _target;
    private float _damage;
    private ObjectPool<GameObject> _bulletPool;

    public void Seek(Transform target, float damage, ObjectPool<GameObject> bulletPool)
    {
        _target = target;
        _damage = damage;
        _bulletPool = bulletPool;
    }

    public void FixedUpdate()
    {
        if (_target == null)
        {
            Destroy(gameObject);
            return;
        }

        var dir = _target.position - transform.position;
        var distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    private void HitTarget()
    {
        var enemy = _target.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(_damage);
        }
        _bulletPool.Release(gameObject);
    }
}