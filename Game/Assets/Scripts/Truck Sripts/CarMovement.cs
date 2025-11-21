using UnityEngine;
using UnityEngine.InputSystem; 

[RequireComponent(typeof(Rigidbody))]
public class CarMovement : MonoBehaviour
{
    [Header("Настройки смещения")]
    [SerializeField] private float sideSpeed = 10f;
    [SerializeField] private float maxSidePosition = 4f;
    [Header("Визуальный эффект поворота")]
    [SerializeField] private float maxSteerAngle = 15f;    
    [SerializeField] private float rotationSpeed = 5f;       
    [SerializeField] private float snapBackSpeed = 20f;

    public float SideSpeed
    {
        get => sideSpeed;
        set => sideSpeed = value;
    }
    
    public float MaxSidePosition
    {
        get => maxSidePosition;
        set => maxSidePosition = value;
    }

    private Rigidbody rb;
    private Vector2 moveInput;

    public void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY |
                         RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ; 
    }
    
    public void OnMove(InputAction.CallbackContext context) => moveInput = context.ReadValue<Vector2>();
    
    public void FixedUpdate()
    {
        var xVelocity = moveInput.x * SideSpeed;
        rb.linearVelocity = new Vector3(xVelocity, rb.linearVelocity.y, 0f);

        var currentPosition = rb.position;
        var clampedX = Mathf.Clamp(currentPosition.x, -MaxSidePosition, MaxSidePosition);
        rb.position = new Vector3(clampedX, currentPosition.y, currentPosition.z);
        

        var targetAngle = moveInput.x * maxSteerAngle;
        
        var currentRotationSpeed = rotationSpeed;
        
        
        var atRightBoundary = currentPosition.x >= MaxSidePosition - 0.01f && moveInput.x > 0;
        var atLeftBoundary = currentPosition.x <= -MaxSidePosition + 0.01f && moveInput.x < 0;

        if (atRightBoundary || atLeftBoundary)
        {
            targetAngle = 0; 
            currentRotationSpeed = snapBackSpeed; 
        }
        else if (moveInput.x == 0) 
            currentRotationSpeed = snapBackSpeed;

        var targetRotation = Quaternion.Euler(0, targetAngle, 0);
        rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, Time.fixedDeltaTime * currentRotationSpeed);
    }
}