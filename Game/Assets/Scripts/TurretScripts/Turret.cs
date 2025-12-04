using System;
using UnityEngine;
using UnityEngine.Pool;

public class Turret : MonoBehaviour
{
    [Header("Ссылки")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private ScriptableObject turretStats;
    private TurretModel turretModel;
    
    private ObjectPool<GameObject> bulletPool;

    public void Awake()
    {
        bulletPool = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(bulletPrefab, firePoint.position, firePoint.rotation), 
            actionOnGet: (obj) => obj.SetActive(true), 
            actionOnRelease: (obj) => obj.SetActive(false), 
            actionOnDestroy: (obj) => Destroy(obj), 
            collectionCheck: false, 
            defaultCapacity: 5, 
            maxSize: 5);
    }

    public void FixedUpdate()
    {
        if (turretModel.CurrentTarget != null)
        {
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
    }

    private void Shoot()
    {
        var bulletGO = bulletPool.Get();
        bulletGO.transform.position = firePoint.position;
        var bullet = bulletGO.GetComponent<Bullet>();
        bullet.Seek(turretModel.CurrentTarget, turretModel.Damage, turretModel.BulletSpeed ,bulletPool);
    }
}