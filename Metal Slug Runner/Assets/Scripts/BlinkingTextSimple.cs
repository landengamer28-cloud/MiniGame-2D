using UnityEngine;
using TMPro;

public class BlinkingTextSimple : MonoBehaviour
{
    [Header("Referencia al texto")]
    public TextMeshProUGUI textMeshPro;

    [Header("Intervalo de parpadeo (segundos)")]
    public float blinkInterval = 0.8f; // Ajusta la velocidad del parpadeo

    private float timer;

    void Update()
    {
        if (textMeshPro == null) return;

        // Usamos tiempo no escalado, así funciona aunque Time.timeScale = 0
        timer += Time.unscaledDeltaTime;

        if (timer >= blinkInterval)
        {
            textMeshPro.enabled = !textMeshPro.enabled;
            timer = 0f;
        }
    }

    void OnEnable()
    {
        // Asegura que el texto empiece visible al activarse
        if (textMeshPro != null)
            textMeshPro.enabled = true;

        timer = 0f;
    }
}
