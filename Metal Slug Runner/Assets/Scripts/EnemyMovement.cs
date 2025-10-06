using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float bounceForce = 8f; // Fuerza de rebote
    public float moveSpeed = 5f;      // Velocidad horizontal
    private Rigidbody2D rb;
    private Vector2 moveDirection;// 1 para derecha, -1 para izquierda

    public AudioClip bounceSound;      // Sonido al chocar con un borde
    private AudioSource audioSource;

    void Start()
    {
        // Obtener el componente Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
        //moveDirection =  new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)); // Comienza moviéndose a la derecha

        // Obtener el componente AudioSource
        audioSource = GetComponent<AudioSource>();
        // Asegurar que el audio no se reproduzca en loop
        audioSource.loop = false;


        do
        {
            moveDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        } while (moveDirection == Vector2.zero);

        rb.AddForce(new Vector2(moveSpeed * moveDirection.x, moveSpeed * moveDirection.y), ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Verificar si el objeto tiene alguno de los tags de borde
        if (collision.gameObject.CompareTag("BRTop") ||
            collision.gameObject.CompareTag("BRBottom") ||
            collision.gameObject.CompareTag("BRRigth") ||
            collision.gameObject.CompareTag("BRLeft"))
        {
            // Reproducir sonido
            if (bounceSound != null)
                audioSource.PlayOneShot(bounceSound);
        }

    }

}
