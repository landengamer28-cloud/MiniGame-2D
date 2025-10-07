using UnityEngine;
using System;

public class PowerUp : MonoBehaviour
{
    public event Action OnCollected; // Evento para avisar al spawner
    public static event Action OnPowerUpCollected; // Evento global

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Detectar colisión con el jugador
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("El jugador recogió el PowerUp");

            // Avisar al spawner
            OnCollected?.Invoke();
            OnPowerUpCollected?.Invoke();

            // Destruir el PowerUp
            Destroy(gameObject);
        }
    }

}
