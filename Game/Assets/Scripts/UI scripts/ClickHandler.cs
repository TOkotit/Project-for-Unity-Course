using UnityEngine;
using UnityEngine.InputSystem;

public class ClickHandler : MonoBehaviour
{       
    [Header("References")]
    [SerializeField] private TurretSystem turretSystem;
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
            
            var ray = mainCamera.ScreenPointToRay(mousePosition);
            var hit = new RaycastHit();

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, enemyLayer))
            {   
                turretSystem.turretSystemModel.ActivateChosenTurret(hit.transform.gameObject);
                
                Debug.Log("Target acquired: " + hit.transform.name);
            }
        }
    }
}