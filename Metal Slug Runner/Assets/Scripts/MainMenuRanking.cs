using UnityEngine;
using TMPro;

public class MainMenuRanking : MonoBehaviour
{
    public TextMeshProUGUI[] scoreTexts; // 5 elementos

    void Start()
    {
        MostrarRanking();
    }

    private void MostrarRanking()
    {
        if (TimeScoreManager.Instance == null) return;

        float[] scores = TimeScoreManager.Instance.LoadScores();

        for (int i = 0; i < scoreTexts.Length; i++)
        {
            if (scores[i] > 0)
                scoreTexts[i].text = $"{i + 1}.  {scores[i]:F2} s";
            else
                scoreTexts[i].text = $"{i + 1}.  ---";
        }
    }

    // Llama a este método desde un botón en la UI para reiniciar las puntuaciones
    public void ResetScores()
    {
        TimeScoreManager.Instance?.ResetScores();
        MostrarRanking();
    }

}
