using UnityEngine;
using Truck_Sripts; // Для доступа к CarStatsSO

namespace Road_scripts
{
    public class WheelVisualUpdater : MonoBehaviour
    {
        [Header("Настройки")]
        [SerializeField]
        private CarStatsSO carStats;

        [SerializeField] private float wheelDiameter = 0.7f;

        [Header("Направление")]
        [Tooltip("Установите -1, если колесо вращается в обратную сторону.")]
        [SerializeField]
        private float rotationDirection = 1f;

        private float _circumference;

        public void Awake()
        {
            if (carStats == null)
            {
                Debug.LogError("WheelVisualUpdater: CarStatsSO не назначен! Невозможно определить скорость.");
                enabled = false;
                return;
            }

            _circumference = Mathf.PI * wheelDiameter;
        }

        public void Update()
        {
            if (!carStats) return;
            
            var distance = carStats.SideSpeed * Time.deltaTime;

            var rotationAngle = (distance / _circumference) * 360f;

            transform.Rotate(rotationAngle * rotationDirection, 0, 0, Space.Self);
        }
    }
}