using UnityEngine;
using UnityEngine.InputSystem;

public class ClickHandler : MonoBehaviour
{   
    [Header("References")]
    [SerializeField] private Turret turret1;
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
            
            var ray = mainCamera.ScreenPointToRay(mousePosition);
            var hit = new RaycastHit();

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, enemyLayer))
            {   
                // переделать!
                turret1.turretModel.CurrentTarget = hit.transform;
                Debug.Log("Target acquired: " + hit.transform.name);
            }
        }
    }
}