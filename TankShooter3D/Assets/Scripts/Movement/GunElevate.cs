﻿using UnityEngine;

namespace TankShooter.Movement
{
    public class GunElevate : MonoBehaviour
    {
        [SerializeField] float elevateForce = 10f;

        [Header("Elevate towards accuracy")]
        [Range(0f, 1f)] [SerializeField] float tolerance = 0.01f;
        [Range(0f, 1f)] [SerializeField] float smoothing = 0.1f;

        Rigidbody rb;
        HingeJoint hj;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            hj = GetComponent<HingeJoint>();
        }

        public void ElevateGun(float input)
        {
            rb.AddTorque(-transform.right * input * elevateForce, ForceMode.VelocityChange);
        }

        public void ElevateGunTowards(Vector3 point)
        {
            Vector3 gunPos = transform.position + hj.anchor;

            Vector3 targetDirection = point - gunPos;

            float dot = Vector3.Dot(transform.up, targetDirection.normalized);
            float positiveDot = Mathf.Abs(dot);

            if (positiveDot < tolerance) return;

            float direction = Mathf.Sign(dot);
            if (positiveDot < smoothing) direction *= (positiveDot / smoothing);

            ElevateGun(direction);
            return;
        }
    }
}

