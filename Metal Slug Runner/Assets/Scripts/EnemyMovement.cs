using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float bounceForce = 8f;     // Fuerza de rebote
    public float moveSpeed = 5f;       // Velocidad horizontal
    public AudioClip bounceSound;      // Sonido al chocar con un borde

    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private AudioSource audioSource;
    private bool isFrozen = false;

    void Start()
    {
        // Obtener componentes
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;

        // Dirección inicial aleatoria
        do
        {
            moveDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        } while (moveDirection == Vector2.zero);

        rb.AddForce(moveDirection.normalized * moveSpeed, ForceMode2D.Impulse);

        // Suscribirse al evento del PowerUp
        PowerUp.OnPowerUpCollected += FreezeFor3Seconds;
    }

    void OnDestroy()
    {
        // Desuscribirse del evento al eliminar el enemigo
        PowerUp.OnPowerUpCollected -= FreezeFor3Seconds;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("BRTop") ||
            collision.gameObject.CompareTag("BRBottom") ||
            collision.gameObject.CompareTag("BRRigth") ||
            collision.gameObject.CompareTag("BRLeft"))
        {
            if (bounceSound != null)
                audioSource.PlayOneShot(bounceSound);
        }
    }

    private void FreezeFor3Seconds()
    {
        if (!gameObject.activeInHierarchy) return;
        StartCoroutine(FreezeCoroutine());
    }

    private IEnumerator FreezeCoroutine()
    {
        if (isFrozen) yield break;
        isFrozen = true;

        // Guardar velocidad y detener movimiento
        Vector2 savedVelocity = rb.linearVelocity;
        rb.linearVelocity = Vector2.zero;
        rb.isKinematic = true;

        yield return new WaitForSeconds(3f); // 3 segundos de pausa

        // Restaurar movimiento
        rb.isKinematic = false;
        rb.linearVelocity = savedVelocity;
        isFrozen = false;
    }

}
