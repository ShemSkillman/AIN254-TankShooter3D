using UnityEngine;
using TMPro;
using System;

namespace TankShooter.Control
{
    public class ControllerSelector : MonoBehaviour
    {
        [SerializeField] Canvas multiplayerCanvas;
        [SerializeField] Canvas singleplayerCanvas;
        [SerializeField] TextMeshProUGUI gunnerControllerButton;
        [SerializeField] TextMeshProUGUI driverControllerButton;
        [SerializeField] TextMeshProUGUI masterControllerButton;

        bool driverHasGamepad = false;

        private void Start()
        {
            GetGameModeDefaults();
            GetMultiplayerControllerDefaults();
            SetButtonText();
        }

        private void GetGameModeDefaults()
        {
            if (!PlayerPrefs.HasKey("GameMode")) return;

            if (PlayerPrefs.GetString("GameMode") == "Singleplayer")
            {
                multiplayerCanvas.gameObject.SetActive(false);
                singleplayerCanvas.gameObject.SetActive(true);
            }
            else if (PlayerPrefs.GetString("GameMode") == "Multiplayer")
            {
                singleplayerCanvas.gameObject.SetActive(false);
                multiplayerCanvas.gameObject.SetActive(true);
            }
        }

        private void GetMultiplayerControllerDefaults()
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
                driverControllerButton.text = "Gamepad";
                gunnerControllerButton.text = "Mouse and Keyboard";

                PlayerPrefs.SetString("DriverInput", "Gamepad");
            }
            else
            {
                driverControllerButton.text = "Mouse and Keyboard";
                gunnerControllerButton.text = "Gamepad";

                PlayerPrefs.SetString("DriverInput", "Mouse and Keyboard");
            }
        }

        public void SwitchControls()
        {
            driverHasGamepad = !driverHasGamepad;
            SetButtonText();
        }

        public void SetMultiplayer()
        {
            PlayerPrefs.SetString("GameMode", "Multiplayer");
        }

        public void SetSingleplayer()
        {
            PlayerPrefs.SetString("GameMode", "Singleplayer");
        }

        
    }
}

