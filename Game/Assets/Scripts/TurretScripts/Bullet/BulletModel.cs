using UnityEngine;

public class BulletModel
{
    private Transform target;
    private float damage;
    private float speed;

    public Transform Target
    {
        get => target;
        set => target = value;
    }

    public float Damage
    {
        get => damage;
        set => damage = value;
    }

    public float Speed
    {
        get => speed;
        set => speed = value;
    }
}
