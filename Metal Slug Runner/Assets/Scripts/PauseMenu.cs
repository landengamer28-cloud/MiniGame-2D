using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("Referencias UI")]
    public GameObject pauseMenuUI;

    private bool isPaused = false;
    private PlayerControls controls;

    private void Awake()
    {
        controls = new PlayerControls();

        // Vinculamos la acción de pausa
        controls.Player.Pause.performed += _ => TogglePause();
    }

    private void OnEnable()
    {
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }

    private void TogglePause()
    {
        if (isPaused)
            Resume();
        else
            Pause();
    }

    public void Resume()
    {
        // Buscar el objeto que sigue al ratón
        FollowMouseUI follower = FindObjectOfType<FollowMouseUI>();
        if (follower != null)
        {
            // Convertir la posición del objeto a coordenadas de pantalla
            Vector3 screenPos = Camera.main.WorldToScreenPoint(follower.transform.position);
            Mouse.current.WarpCursorPosition(new Vector2(screenPos.x, screenPos.y));
        }

        // Ahora reanudar el juego normalmente
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void QuitGame()
    {
        Time.timeScale = 1f; // Restaurar el tiempo antes de cambiar escena
        SceneManager.LoadScene("MainMenu");
    }
}
