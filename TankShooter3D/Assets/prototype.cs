using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class prototype : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject overlayCanvas;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }

        if (player == null)
        {
            overlayCanvas.SetActive(false);
        }
    }
}
