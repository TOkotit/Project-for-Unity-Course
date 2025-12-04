using System;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{   
    private BulletModel bulletModel;
    private ObjectPool<Bullet> pool;

    public Bullet()
    {
        bulletModel = new BulletModel();
    }

    public void Seek(Transform bulletTarget, float bulletDamage, 
        float bulletSpeed,  ObjectPool<Bullet> bulletPool)
    {
        bulletModel.Target = bulletTarget;
        bulletModel.Damage = bulletDamage;
        pool = bulletPool;
        bulletModel.Speed = bulletSpeed;
    }

    public void FixedUpdate()
    {
        if (bulletModel.Target is null)
        {
            pool.Release(this);
            return;
        }

        var dir = bulletModel.Target.position - transform.position;
        var distanceThisFrame = bulletModel.Speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    private void HitTarget()
    {
        pool.Release(this);
    }
}