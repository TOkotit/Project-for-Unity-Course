using System;
using UnityEngine;
using UnityEngine.Pool;

public class Turret : MonoBehaviour
{
    [Header("Ссылки")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    
    public TurretModel turretModel;
    
    private ObjectPool<Bullet> bulletPool;

    public Turret()
    {
        turretModel = new TurretModel();
    }
    public void Awake()
    {
        bulletPool = new ObjectPool<Bullet>(
            createFunc: () => Instantiate(bulletPrefab, firePoint.position, firePoint.rotation).GetComponent<Bullet>(), 
            actionOnGet: (obj) => obj.gameObject.SetActive(true), 
            actionOnRelease: (obj) => obj.gameObject.SetActive(false), 
            actionOnDestroy: (obj) => Destroy(obj.gameObject), 
            collectionCheck: false, 
            defaultCapacity: 5, 
            maxSize: 5);
    }

    public void FixedUpdate()
    {
        if (turretModel.CurrentTarget is null)
        {
            return;
        }

        // Поворот к цели
        var dir = turretModel.CurrentTarget.position - transform.position;
        var lookRotation = Quaternion.LookRotation(dir);
        var rotation = Quaternion.Lerp(transform.rotation, lookRotation, 
            turretModel.RotationSpeed * Time.deltaTime).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        // Стрельба
        if (turretModel.FireCountdown <= 0f)
        {
            Shoot();
            turretModel.FireCountdown = 1f / turretModel.FireRate;
        }
        turretModel.FireCountdown -= Time.deltaTime;
    }

    private void Shoot()
    {
        var bulletGo = bulletPool.Get();
        bulletGo.transform.position = firePoint.position;
        bulletGo.Seek(turretModel.CurrentTarget, turretModel.Damage, turretModel.BulletSpeed, bulletPool);
    }
}