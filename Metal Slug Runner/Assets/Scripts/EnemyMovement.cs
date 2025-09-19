using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float bounceForce = 8f; // Fuerza de rebote
    public float moveSpeed = 5f;      // Velocidad horizontal
    private Rigidbody2D rb;
    private int moveDirection = 1; // 1 para derecha, -1 para izquierda

    void Start()
    {
        // Obtener el componente Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Mantener siempre movimiento horizontal
        rb.linearVelocity = new Vector2(moveSpeed * moveDirection, rb.linearVelocity.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Detecta si chocó contra el suelo o el techo para rebotar
        if (collision.gameObject.CompareTag("BRBottom") || collision.gameObject.CompareTag("BRTop"))
        {
            float direction = collision.gameObject.CompareTag("BRBottom") ? 1f : -1f;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, bounceForce * direction);
        }

        // Cambiar dirección si toca una pared o a otro enemigo
        if (collision.gameObject.CompareTag("BRLeft") || collision.gameObject.CompareTag("BRRigth") || collision.gameObject.CompareTag("Enemy"))
        {
            moveDirection *= -1; // Cambia de derecha a izquierda y viceversa
        }
    }
}
