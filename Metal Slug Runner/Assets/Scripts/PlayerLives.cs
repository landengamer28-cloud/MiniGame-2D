using UnityEngine;
using TMPro; // TextMeshPro para UI de texto
using UnityEngine.SceneManagement; // Para recargar la escena
using UnityEngine.UI; // Para botones
using System.Collections; // Para corrutinas


public class PlayerLives : MonoBehaviour
{
    [Header("Vidas")]
    public int lives = 3; // Número inicial de vidas
    public TextMeshProUGUI livesText; // Texto UI para mostrar vidas

    [Header("Game Over UI")]
    public GameObject gameOverUI; // Panel de Game Over
    public Button restartButton; // Botón de reinicio
    public Button quitButton; // Botón de salir

    [Header("Transición visual")]
    public CanvasGroup fadePanel; // Panel para el fade
    public float fadeDuration = 1.5f; // Duración del fade

    [Header("Música")]
    public AudioSource musicSource;      // Música principal
    public AudioClip gameOverMusic;      // Música de Game Over

    [Header("Sonido de Muerte")]
    public AudioSource sfxSource;        // Fuente para efectos (gritos, etc.)
    public AudioClip deathScream;        // Grito de muerte

    private bool isGameOver = false;

    void Start()
    {
        // Inicializar UI de vidas
        UpdateLivesUI();

        // Ocultar UI de Game Over al inicio
        if (gameOverUI != null)
            gameOverUI.SetActive(false);

        // Configurar botones
        if (restartButton != null)
            restartButton.onClick.AddListener(RestartGame);
        
        if (quitButton != null)
            quitButton.onClick.AddListener(QuitGame);

        // Asegurar que el panel de fade está invisible al inicio
        if (fadePanel != null)
            fadePanel.alpha = 0f;
    }

    // Detectar colisiones con enemigos
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Si colisiona con un enemigo, perder una vida
        if (!isGameOver && collision.gameObject.CompareTag("Enemy"))
        {
            LoseLife();
        }
    }

    // Manejar la pérdida de una vida
    void LoseLife()
    {
        lives--;
        UpdateLivesUI();

        if (lives <= 0)
        {
            StartCoroutine(HandleGameOver());
        }
    }

    // Actualizar el texto de vidas en la UI
    void UpdateLivesUI()
    {
        if (livesText != null)
            livesText.text = "Vidas: " + lives;
    }

    // Manejar la secuencia de Game Over
    IEnumerator HandleGameOver()
    {
        isGameOver = true;
        Time.timeScale = 0f;

        // Detener el contador de tiempo y guardar el tiempo actual
        TimeScoreManager.Instance.StopCounting();
        TimeScoreManager.Instance.SaveCurrentTime();


        // Reproducir grito de muerte
        if (sfxSource != null && deathScream != null)
        {
            sfxSource.PlayOneShot(deathScream);
        }

        // Pequeña pausa antes de empezar el fade (para dejar sonar el grito)
        yield return new WaitForSecondsRealtime(0.7f);

        float elapsed = 0f;
        float startVolume = musicSource != null ? musicSource.volume : 0f;

        // Fade-out música + pantalla
        while (elapsed < fadeDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = elapsed / fadeDuration;

            if (fadePanel != null)
                fadePanel.alpha = Mathf.Lerp(0f, 1f, t);

            if (musicSource != null)
                musicSource.volume = Mathf.Lerp(startVolume, 0f, t);

            yield return null;
        }

        // Asegurar estado final
        if (fadePanel != null) fadePanel.alpha = 1f;
        if (musicSource != null)
        {
            musicSource.Stop();
            musicSource.volume = startVolume;
        }

        // Música de Game Over
        if (musicSource != null && gameOverMusic != null)
        {
            musicSource.clip = gameOverMusic;
            musicSource.loop = false;
            musicSource.Play();
        }
        // Mostrar UI de Game Over
        if (gameOverUI != null)
            gameOverUI.SetActive(true);
    }

    // Reiniciar el juego recargando la escena actual
    public void RestartGame()
    {
        if (TimeScoreManager.Instance != null)
            TimeScoreManager.Instance.ResetCurrentTime(); // Reiniciar tiempo

        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Salir del juego
    public void QuitGame()
    {
        Time.timeScale = 1f; // Restaurar el tiempo antes de cambiar escena
        SceneManager.LoadScene("MainMenu");
    }
}
