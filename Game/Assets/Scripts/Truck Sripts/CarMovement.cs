using UnityEngine;
using UnityEngine.InputSystem; 

[RequireComponent(typeof(Rigidbody))]
public class CarMovement : MonoBehaviour
{
    [Header("Настройки смещения")]
    [SerializeField] private float sideSpeed = 10f;
    [SerializeField] private float maxSidePosition = 4f;
    [Header("Визуальный эффект поворота")]
    [SerializeField] private float maxSteerAngle = 15f;    // Максимальный угол поворота (Y-ось)
    [SerializeField] private float rotationSpeed = 5f;       // Скорость поворота при активном вводе
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
        
        // ----------------------------------------
        
        // --- 2. ВИЗУАЛЬНОЕ ВРАЩЕНИЕ (ЭФФЕКТ ПОВОРОТА) ---

        // 1. Вычисляем целевой угол
        float targetAngle = moveInput.x * maxSteerAngle;
        
        // 2. Определяем текущую скорость вращения (по умолчанию - скорость поворота)
        float currentRotationSpeed = rotationSpeed;
        
        // --- БЛОКИРОВКА ПОВОРОТА У ГРАНИЦЫ И БЫСТРЫЙ ВОЗВРАТ ---
        
        // Проверяем, находится ли машина у границы И пытается ли игрок двигаться дальше.
        // Используем небольшую дельту (0.01f), чтобы учесть погрешности float.
        bool atRightBoundary = currentPosition.x >= MaxSidePosition - 0.01f && moveInput.x > 0;
        bool atLeftBoundary = currentPosition.x <= -MaxSidePosition + 0.01f && moveInput.x < 0;

        if (atRightBoundary || atLeftBoundary)
        {
            // Если машина достигла предела и продолжает на него давить,
            // целевой угол должен быть 0, чтобы машина не поворачивалась или выравнивалась.
            targetAngle = 0; 
            currentRotationSpeed = snapBackSpeed; // Быстрое выравнивание у стены
        }
        else if (moveInput.x == 0) 
        {
            // Если ввода нет, быстро возвращаем машину в прямое положение (0 градусов).
            currentRotationSpeed = snapBackSpeed;
        }

        Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
        rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, Time.fixedDeltaTime * currentRotationSpeed);
    }
}