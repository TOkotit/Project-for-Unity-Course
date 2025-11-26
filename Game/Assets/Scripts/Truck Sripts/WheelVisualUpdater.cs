using UnityEngine;

public class WheelVisualUpdater : MonoBehaviour
{
    private WheelCollider targetWheelCollider;
    
    [SerializeField] private float visualCorrectionAngle = 0f;

    public void Awake()
    {
        targetWheelCollider = GetComponent<WheelCollider>();
        if (targetWheelCollider is not null) return;
        Debug.LogError("На этом объекте нет WheelCollider!");
        enabled = false;
    }

    public void LateUpdate()
    {
        if (targetWheelCollider is null) return;

        Vector3 position;
        Quaternion rotation;
        
        targetWheelCollider.GetWorldPose(out position, out rotation);
        
        
        var finalRotation = rotation;
        
        if (Mathf.Abs(visualCorrectionAngle) > 0.1f)
        {
            var correction = Quaternion.Euler(0, visualCorrectionAngle, 0);
            
            finalRotation *= correction; 
        }

        transform.rotation = finalRotation;
    }
}