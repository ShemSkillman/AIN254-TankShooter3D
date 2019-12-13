using UnityEngine;

namespace TankShooter.Control
{
    public class ControllerCreator : MonoBehaviour
    {
        private void Start()
        {
            string driverinput = PlayerPrefs.GetString("DriverInput");

            if (driverinput == "Gamepad")
            {
                gameObject.AddComponent<GamepadDriverController>();
                gameObject.AddComponent<MKbGunnerController>();
            }
            else
            {
                gameObject.AddComponent<GamepadGunnerController>();
                gameObject.AddComponent<MKbDriverController>();
            }
        }
    }
}

