using UnityEngine;
using TMPro;

public class GameTimerUI : MonoBehaviour
{
    [Header("Referencia al texto TMP")]
    public TextMeshProUGUI timeText;

    void Update()
    {
        if (TimeScoreManager.Instance != null)
        {
            float tiempo = TimeScoreManager.Instance.GetCurrentTime();
            timeText.text = $"Tiempo: {tiempo:F2} s";
        }
    }
}
