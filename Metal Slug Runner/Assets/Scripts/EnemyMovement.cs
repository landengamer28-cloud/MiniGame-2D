using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float bounceForce = 8f;
    public float moveSpeed = 5f;
    public AudioClip bounceSound;

    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private AudioSource audioSource;
    private bool isFrozen = false;
    private bool isBoosted = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;

        // Dirección inicial aleatoria
        do
        {
            moveDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        } while (moveDirection == Vector2.zero);

        rb.AddForce(moveDirection.normalized * moveSpeed, ForceMode2D.Impulse);

        // Suscribirse a ambos eventos
        PowerUp.OnPowerUpCollected += FreezeFor3Seconds;
        PowerUp.OnSpeedBoostCollected += BoostSpeedFor3Seconds;
    }

    void OnDestroy()
    {
        PowerUp.OnPowerUpCollected -= FreezeFor3Seconds;
        PowerUp.OnSpeedBoostCollected -= BoostSpeedFor3Seconds;
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

    // Congelar enemigos
    private void FreezeFor3Seconds()
    {
        if (!gameObject.activeInHierarchy) return;
        StartCoroutine(FreezeCoroutine());
    }

    private IEnumerator FreezeCoroutine()
    {
        if (isFrozen) yield break;
        isFrozen = true;

        Vector2 savedVelocity = rb.linearVelocity;
        rb.linearVelocity = Vector2.zero;
        rb.isKinematic = true;

        yield return new WaitForSeconds(3f);

        rb.isKinematic = false;
        rb.linearVelocity = savedVelocity;
        isFrozen = false;
    }

    // Duplicar velocidad temporalmente
    private void BoostSpeedFor3Seconds()
    {
        if (!gameObject.activeInHierarchy) return;
        StartCoroutine(SpeedBoostCoroutine());
    }

    private IEnumerator SpeedBoostCoroutine()
    {
        if (isBoosted) yield break;
        isBoosted = true;

        float originalSpeed = moveSpeed;
        moveSpeed *= 2f;
        rb.linearVelocity *= 2f;

        // Opcional: feedback visual
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Color originalColor = sr.color;
        sr.color = Color.red;

        yield return new WaitForSeconds(3f);

        moveSpeed = originalSpeed;
        rb.linearVelocity /= 2f;
        sr.color = originalColor;
        isBoosted = false;
    }
}

