using UnityEngine;
using TMPro;

namespace TankShooter.Control
{
    public class ControllerSelector : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI gunnerInputButton;
        [SerializeField] TextMeshProUGUI driverInputButton;

        bool driverHasGamepad = false;

        private void Start()
        {
            GetDefaults();
            SetButtonText();
        }

        private void GetDefaults()
        {
            if (!PlayerPrefs.HasKey("DriverInput")) return;

            if (PlayerPrefs.GetString("DriverInput") == "Gamepad")
            {
                driverHasGamepad = true;
            }
            else if (PlayerPrefs.GetString("DriverInput") == "Mouse and Keyboard")
            {
                driverHasGamepad = false;
            }
        }

        private void SetButtonText()
        {
            if (driverHasGamepad)
            {
                driverInputButton.text = "Gamepad";
                gunnerInputButton.text = "Mouse and Keyboard";

                PlayerPrefs.SetString("DriverInput", "Gamepad");
            }
            else
            {
                driverInputButton.text = "Mouse and Keyboard";
                gunnerInputButton.text = "Gamepad";

                PlayerPrefs.SetString("DriverInput", "Mouse and Keyboard");
            }
        }

        public void SwitchControls()
        {
            driverHasGamepad = !driverHasGamepad;
            SetButtonText();
        }

        
    }
}

