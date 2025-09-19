using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Movimiento")]
    public float speed = 3f;
    public float jumpForce = 8f;
    private int direction = 1;     // 1 = derecha, -1 = izquierda

    [Header("Detección")]
    public Transform wallCheckRight;   // Empty a la derecha
    public Transform wallCheckLeft;    // Empty a la izquierda
    public Transform groundCheck;      // Empty debajo del enemigo
    public float checkDistance = 0.1f; // Distancia de raycast
    public LayerMask groundLayer;      // Suelo y paredes

    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // 🔹 Movimiento horizontal
        rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);

        // 🔹 Si voy a la derecha, usa wallCheckRight
        if (direction == 1 && Physics2D.Raycast(wallCheckRight.position, Vector2.right, checkDistance, groundLayer))
        {
            direction = -1;
        }

        // 🔹 Si voy a la izquierda, usa wallCheckLeft
        if (direction == -1 && Physics2D.Raycast(wallCheckLeft.position, Vector2.left, checkDistance, groundLayer))
        {
            direction = 1;
        }

        // 🔹 Detectar suelo debajo → rebote
        if (Physics2D.Raycast(groundCheck.position, Vector2.down, checkDistance, groundLayer))
        {
            if (rb.linearVelocity.y <= 0f)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        // Rayos de depuración
        if (wallCheckRight != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(wallCheckRight.position, wallCheckRight.position + Vector3.right * checkDistance);
        }

        if (wallCheckLeft != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(wallCheckLeft.position, wallCheckLeft.position + Vector3.left * checkDistance);
        }

        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * checkDistance);
        }
    }
}
