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
        // Mover el objeto hacia la posición del ratón siempre que no sobrepase los límites de la pantalla

        mover();

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

    private void mover()
    {
        // Posición del ratón
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10f));

        // Obtener límites de la cámara en coordenadas del mundo
        Vector3 minPantalla = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 10f));
        Vector3 maxPantalla = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 10f));

        // Limitar la posición del objeto dentro de los bordes
        worldPosition.x = Mathf.Clamp(worldPosition.x, minPantalla.x, maxPantalla.x);
        worldPosition.y = Mathf.Clamp(worldPosition.y, minPantalla.y, maxPantalla.y);

        transform.position = new Vector3(worldPosition.x, worldPosition.y, 0f);
    }

}
