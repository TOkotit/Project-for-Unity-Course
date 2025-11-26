using UnityEngine;

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

    public void Update()
    {
        if (currentTarget != null)
        {
            // Поворот к цели
            Vector3 dir = currentTarget.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime).eulerAngles;
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
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        if (bullet != null)
            bullet.Seek(currentTarget, damage);
    }

    public void SetTarget(Transform target)
    {
        currentTarget = target;
    }
}