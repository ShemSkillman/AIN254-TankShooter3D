using TMPro;
using UnityEngine;

public class SpeedUI : MonoBehaviour
{
    TextMeshProUGUI speedText;
    [SerializeField] Rigidbody playerTank;

    private void Awake()
    {
        speedText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        Vector3 localVelocity = playerTank.transform.InverseTransformDirection(playerTank.velocity);
        float speedKmH = (localVelocity.magnitude / 1000) * 3600 * Mathf.Sign(localVelocity.z);
        speedText.text = string.Format("{0:0} km/h", speedKmH);
    }
}
