using System;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class RoadTile : MonoBehaviour
{
    private float tileLength;
    private Rigidbody rb;
    private Collider col;
    private Bounds worldBounds;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();

        // Рекомендуется: управлять тайлами вручную — кинематический ригидбоди
        rb.isKinematic = true;
        rb.interpolation = RigidbodyInterpolation.None;
    }

    private void UpdateBounds() => worldBounds = col.bounds;

    private void Initialize()
    {
        rb = GetComponent<Rigidbody>();
        var boxCollider = GetComponent<BoxCollider>();
        if (boxCollider != null)
            tileLength = boxCollider.bounds.size.z;
        else
            Debug.LogError("На объекте RoadTile отсутствует BoxCollider!");
    }
    
    
    public void SetPhysicsVelocity(float speed)
    {
        if (rb is null) return;
        var shift = Vector3.back * (speed * Time.fixedDeltaTime); // <-- fixedDeltaTime!
        rb.MovePosition(rb.position + shift);
    }
    
    public Bounds GetWorldBounds()
    {
        UpdateBounds();
        return col.bounds;
    }
    
    public float LeadingEdgeOffset()
    {
        UpdateBounds();
        return transform.position.z - GetWorldBounds().min.z;
    }
    
    public void RecycleToZ(float newZ)
    {
        // помним оффсет: хотим, чтобы bounds.min.z == newMaxZ (без зазора)
        float offset = LeadingEdgeOffset();
        float targetPosZ = newZ + offset;
        Vector3 target = new Vector3(transform.position.x, transform.position.y, targetPosZ);

        // Телепортим физически, синхронизируем
        rb.position = target;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        // и ещё MovePosition чтобы физика "поняла" новое место
        rb.MovePosition(target);
        UpdateBounds();
    }
    
    public void Recycle(Vector3 newPosition)
    {
        rb.position = newPosition;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        rb.MovePosition(newPosition);
    }
    
    public float GetTileLength() => GetWorldBounds().size.z;
}
