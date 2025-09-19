using UnityEngine;
using UnityEngine.InputSystem;

public class FollowMouseUI : MonoBehaviour
{

    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Posición del ratón
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10f));

        // Mover usando física
        rb.MovePosition(worldPosition);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Si colisiona con un Enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("¡Jugador detectó un enemigo! Se cierra el juego.");
            Application.Quit(); // Cierra el juego
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // Detiene el juego en el editor
#endif

        }
    }
}
