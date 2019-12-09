using System.Collections;
using TMPro;
using UnityEngine;

public class TimeUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;

    private void Start()
    {
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        int seconds = 0;

        while (true)
        {
            yield return new WaitForSeconds(1f);

            seconds += 1;

            int hours = seconds / 3600;
            seconds -= hours * 3600;

            int minutes = seconds / 60;
            seconds -= minutes * 60;

            timerText.text = string.Format("Time elapsed:\n{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
        }
        
    }
}
