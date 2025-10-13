using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("UI Buttons")]
    public UnityEngine.UI.Button startButton;
    public UnityEngine.UI.Button quitButton;

    [Header("Opciones")]
    public string gameSceneName = "GameScene"; // Nombre de la escena del juego

    private void Start()
    {
        // Asignar funciones a los botones
        startButton.onClick.AddListener(StartGame);
        quitButton.onClick.AddListener(QuitGame);
    }

    private void StartGame()
    {
        if (TimeScoreManager.Instance != null)
            TimeScoreManager.Instance.ResetCurrentTime(); // Reiniciar tiempo

        // Cambiar a la escena del juego
        SceneManager.LoadScene(gameSceneName);
    }

    private void QuitGame()
    {
        // Funciona solo en build; en editor no cierra el juego
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
