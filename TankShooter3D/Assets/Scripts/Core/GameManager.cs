using UnityEngine.SceneManagement;
using UnityEngine;

namespace TankShooter.Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] Canvas[] canvases;
        int currentCanvasIndex = 0;

        public void NextScene()
        {
            int currentIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(++currentIndex);
        }

        public void Quit()
        {
            Application.Quit();
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadScene("Main menu");
        }

        public void NextCanvas()
        {
            canvases[currentCanvasIndex].gameObject.SetActive(false);

            if (currentCanvasIndex >= canvases.Length) return;
            currentCanvasIndex++;

            canvases[currentCanvasIndex].gameObject.SetActive(true);
        }
    }
}
