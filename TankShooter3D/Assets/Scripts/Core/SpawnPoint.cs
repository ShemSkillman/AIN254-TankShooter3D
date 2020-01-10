using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankShooter.Core
{
    public class SpawnPoint : MonoBehaviour
    {
        public bool IsFree { get; set; } = true;

        private void OnTriggerEnter(Collider other)
        {
            IsFree = false;
        }

        private void OnTriggerExit(Collider other)
        {
            IsFree = true;
        }
    }
}

