using UnityEngine;
using System;
using System.Linq;

public class TimeScoreManager : MonoBehaviour
{
    public static TimeScoreManager Instance;

    private float currentTime;
    private bool isCounting = true;

    private const int maxScores = 5;
    private const string prefsKey = "TopTimes";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (isCounting)
            currentTime += Time.deltaTime;
    }

    public void StopCounting() => isCounting = false;
    public void StartCounting() => isCounting = true;

    public void ResetCurrentTime()
    {
        currentTime = 0f;
        isCounting = true;
    }

    public float GetCurrentTime() => currentTime;

    public void SaveCurrentTime()
    {
        float[] scores = LoadScores();
        float newTime = currentTime;

        scores = scores.Concat(new float[] { newTime })
                       .OrderByDescending(t => t)
                       .Take(maxScores)
                       .ToArray();

        for (int i = 0; i < maxScores; i++)
            PlayerPrefs.SetFloat($"{prefsKey}_{i}", scores[i]);

        PlayerPrefs.Save();
    }

    public float[] LoadScores()
    {
        float[] scores = new float[maxScores];
        for (int i = 0; i < maxScores; i++)
            scores[i] = PlayerPrefs.GetFloat($"{prefsKey}_{i}", 0f);
        return scores;
    }

    public void ResetScores()
    {
        for (int i = 0; i < maxScores; i++)
            PlayerPrefs.DeleteKey($"{prefsKey}_{i}");
    }
}
