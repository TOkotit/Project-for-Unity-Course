using UnityEngine;

public class Bullet : MonoBehaviour
{   
    [Header("Bullet Parameters")]
    [SerializeField] private float speed = 70f;
    private Transform target;
    private float damage;

    public void Seek(Transform _target, float _damage)
    {
        target = _target;
        damage = _damage;
    }

    public void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    private void HitTarget()
    {
        Enemy enemy = target.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}