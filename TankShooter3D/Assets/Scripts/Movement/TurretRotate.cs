﻿using UnityEngine;

namespace TankShooter.Movement
{
    public class TurretRotate : MonoBehaviour
    {
        [SerializeField] float turretRotationSpeed = 10f;

        [Header("Rotate towards accuracy")]
        [Range(0f, 1f)] [SerializeField] float tolerance = 0.01f;
        [Range(0f, 1f)] [SerializeField] float smoothing = 0.1f;

        Rigidbody rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void RotateTurret(float direction)
        {
            rb.AddTorque(transform.up * direction * turretRotationSpeed * rb.mass);
        }

        public void RotateTurretTowards(Vector3 point)
        {
            Vector3 targetDirection = point - transform.position;
            
            float dot = Vector3.Dot(transform.right, targetDirection.normalized);
            float positiveDot = Mathf.Abs(dot);
            
            if (positiveDot < tolerance) return;

            float direction = Mathf.Sign(dot);
            if (positiveDot < smoothing) direction *= (positiveDot / smoothing);

            RotateTurret(direction);
            return;
        }
    }
}

