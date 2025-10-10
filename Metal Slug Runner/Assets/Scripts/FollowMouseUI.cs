using UnityEngine;
using UnityEngine.InputSystem;

public class FollowMouseUI : MonoBehaviour
{
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Cursor.visible = false;
    }

    void Update()
    {
        // Solo mover si el juego NO est� pausado
        if (Time.timeScale > 0f)
        {
            Mover();
        }
    }

    private void Mover()
    {
        // Posici�n del rat�n
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10f));

        // Obtener l�mites de la c�mara en coordenadas del mundo
        Vector3 minPantalla = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 10f));
        Vector3 maxPantalla = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 10f));

        // Limitar la posici�n del objeto dentro de los bordes
        worldPosition.x = Mathf.Clamp(worldPosition.x, minPantalla.x, maxPantalla.x);
        worldPosition.y = Mathf.Clamp(worldPosition.y, minPantalla.y, maxPantalla.y);

        transform.position = new Vector3(worldPosition.x, worldPosition.y, 0f);
    }
}
