using UnityEngine;
using UnityEngine.InputSystem;

public class ClickHandler : MonoBehaviour
{   
    [Header("References")]
    [SerializeField] private Turret turret;
    [SerializeField] private LayerMask enemyLayer;
    
    private Camera mainCamera;

    public void Awake()
    {
        mainCamera = Camera.main;
    }

    public void OnShoot(InputAction.CallbackContext context)
    {   
        if (context.performed)
        {
            var mousePosition = context.ReadValue<Vector2>();
            Debug.Log(mousePosition);
            
            Ray ray = mainCamera.ScreenPointToRay(mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, enemyLayer))
            {
                turret.SetTarget(hit.transform);
                Debug.Log("Target acquired: " + hit.transform.name);
            }
        }
    }
}