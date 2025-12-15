using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Scripts.Entities; 
using Truck_Sripts;
using UnityEngine.Serialization;


namespace Entities.PlayerScripts
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Конфигурация Автомобиля")] 
        [SerializeField] private CarStatsSO carStats;

        private Player _playerModel;
        private Rigidbody _rb;
        private Vector2 _moveInput;


        public void Awake()
        {
            if (carStats is null)
            {
                carStats = Resources.Load<CarStatsSO>("Config/CarStatsSO");
            }

            if (Game.Instance != null)
            {
                _playerModel = Game.Instance.playerModel;
            }
            else
            { 
                _playerModel.Initialize(carStats);
            }
            

            _rb = GetComponent<Rigidbody>();

            _rb.constraints = _playerModel.constraints;

            _playerModel.ImJustDie.AddListener(HandleDeath);
        }
        


        public void OnMove(InputAction.CallbackContext context)
        {
            _moveInput = context.ReadValue<Vector2>();
        }

        public void OnSwitchWeapon(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            var newIndex = 1 - _playerModel.CurrentWeaponIndex;
            _playerModel.SwitchWeapon(newIndex);
        }


        public void FixedUpdate()
        {
            var xVelocity = _moveInput.x * _playerModel.SideSpeed;
            _rb.linearVelocity = new Vector3(xVelocity, _rb.linearVelocity.y, 0f); 

            var currentPosition = _rb.position;
            var clampedX = Mathf.Clamp(currentPosition.x, -_playerModel.maxSidePosition, _playerModel.maxSidePosition);
            
            _rb.position = new Vector3(clampedX, _playerModel.fixedYPosition, currentPosition.z);

            ApplySteerRotation(currentPosition);
        }

        private void ApplySteerRotation(Vector3 currentPosition)
        {
            var targetAngle = _moveInput.x * _playerModel.maxSteerAngle;
            var currentRotationSpeed = _playerModel.rotationSpeed;

            var atRightBoundary = currentPosition.x >= _playerModel.maxSidePosition - 0.01f && _moveInput.x > 0;
            var atLeftBoundary = currentPosition.x <= -_playerModel.maxSidePosition + 0.01f && _moveInput.x < 0;

            if (atRightBoundary || atLeftBoundary || _moveInput.x == 0)
            {
                targetAngle = 0;
                currentRotationSpeed = _playerModel.snapBackSpeed;
            }

            var targetRotation = Quaternion.Euler(0, targetAngle, 0);
            _rb.rotation = Quaternion.Slerp(_rb.rotation, targetRotation, Time.fixedDeltaTime * currentRotationSpeed);
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
                _playerModel.TakeDamage(10f);
        }
        
        private void HandleDeath()
        {
            Debug.Log("You dead.");
        }
    }
}