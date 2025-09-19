using UnityEngine;
using UnityEngine.InputSystem;

public class FollowMouseUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Lee la posición del ratón con el nuevo sistema
        Vector2 mousePosition = Mouse.current.position.ReadValue();

        // Convierte la posición a mundo
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10f));

        // Mueve el objeto
        transform.position = worldPosition;
    }
}
