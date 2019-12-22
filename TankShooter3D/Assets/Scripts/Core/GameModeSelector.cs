using UnityEngine;

namespace TankShooter.Core
{
    public class GameModeSelector : MonoBehaviour
    {
        [SerializeField] GameObject singleplayerCore;
        [SerializeField] GameObject multiplayerCore;
        [SerializeField] Transform spawnPoint;

        private void Awake()
        {
            if (PlayerPrefs.GetString("GameMode") == "Singleplayer")
            {
                Instantiate(singleplayerCore, spawnPoint.position, Quaternion.identity);
            }
            else if (PlayerPrefs.GetString("GameMode") == "Multiplayer")
            {
                Instantiate(multiplayerCore, spawnPoint.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }
}

