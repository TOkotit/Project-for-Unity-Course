using System;
using UnityEngine;
using UnityEngine.Pool;

public class Turret : MonoBehaviour
{
    [Header("Turret Parameters")]
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float damage = 25f;
    [Header("References")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    
    private Transform currentTarget;
    private float fireCountdown = 0f;
    private ObjectPool<GameObject> bulletPool;

    public void Awake()
    {
        bulletPool = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(bulletPrefab, firePoint.position, firePoint.rotation), 
            actionOnGet: (obj) => obj.SetActive(true), 
            actionOnRelease: (obj) => obj.SetActive(false), 
            actionOnDestroy: (obj) => Destroy(obj), 
            collectionCheck: false, 
            defaultCapacity: 10, 
            maxSize: 10);
    }

    public void FixedUpdate()
    {
        if (currentTarget != null)
        {
            // Поворот к цели
            var dir = currentTarget.position - transform.position;
            var lookRotation = Quaternion.LookRotation(dir);
            var rotation = Quaternion.Lerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime).eulerAngles;
            transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);

            // Стрельба
            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }
            fireCountdown -= Time.deltaTime;
        }
    }

    private void Shoot()
    {
        var bulletGO = bulletPool.Get();
        bulletGO.transform.position = firePoint.position;
        var bullet = bulletGO.GetComponent<Bullet>();
        if (bullet != null)
            bullet.Seek(currentTarget, damage, bulletPool);
    }

    public void SetTarget(Transform target)
    {
        currentTarget = target;
    }
}