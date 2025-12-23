using System;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{   
    private ObjectPool<Bullet> pool;

    private Transform target;
    private EnemyModel enemy;
    private float damage;
    private float speed;

    public void Seek(Transform bulletTarget, EnemyModel enemyTarget, float bulletDamage, 
        float bulletSpeed,  ObjectPool<Bullet> bulletPool)
    {
        target = bulletTarget;
        enemy = enemyTarget;
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
        enemy.TakeDamage(damage);
        pool.Release(this);
    }
}