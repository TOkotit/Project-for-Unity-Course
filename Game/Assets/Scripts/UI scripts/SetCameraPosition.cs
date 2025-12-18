using System;
using UnityEngine;

public class SetCameraPosition : MonoBehaviour
{
    [Header("Position")]
    [SerializeField] private float PositionX;
    [SerializeField] private float PositionY;
    [SerializeField] private float PositionZ;
    [Header("Rotation")]
    [SerializeField] private float RotationX;
    [SerializeField] private float RotationY;
    [SerializeField] private float RotationZ;


    public void Update()
    {
        transform.position = new Vector3(PositionX, PositionY, PositionZ);
        transform.rotation = Quaternion.Euler(RotationX, RotationY, RotationZ);
    }
}
