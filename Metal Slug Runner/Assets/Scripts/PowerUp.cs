using UnityEngine;
using System;

public class PowerUp : MonoBehaviour
{
    public event Action OnCollected; // Evento para avisar al spawner

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("El jugador recogió el PowerUp");

            // Avisar al spawner
            OnCollected?.Invoke();

            // Destruir el PowerUp
            Destroy(gameObject);
        }
    }
}
