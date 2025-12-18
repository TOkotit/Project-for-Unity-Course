using UnityEngine;
using UnityEngine.Pool;

public class EnemyBullet : MonoBehaviour
{
    private ObjectPool<EnemyBullet> pool;

    private Transform target;
    private float damage;
    private float speed;

    public void Seek(Transform bulletTarget, float bulletDamage, 
        float bulletSpeed,  ObjectPool<EnemyBullet> bulletPool)
    {
        target = bulletTarget;
        damage = bulletDamage;
        pool = bulletPool;
        speed = bulletSpeed;
    }

    public void FixedUpdate()
    {
        if (target == null)
        {
            pool.Release(this);
            return;
        }

        var dir = target.position - transform.position;
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
        //нанесение урона
        pool.Release(this);
    }
}
