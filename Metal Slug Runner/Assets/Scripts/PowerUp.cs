using UnityEngine;
using System;

public class PowerUp : MonoBehaviour
{
    public enum PowerUpType { Freeze, SpeedBoost } // Nuevo enum
    public PowerUpType type = PowerUpType.Freeze;  // Puedes elegir en el Inspector

    public event Action OnCollected; // Evento local para el spawner
    public static event Action OnPowerUpCollected; // Evento global (freeze)
    public static event Action OnSpeedBoostCollected; // Nuevo evento global

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log($"El jugador recogió el PowerUp de tipo: {type}");

            OnCollected?.Invoke();

            // Lanzar evento según el tipo
            switch (type)
            {
                case PowerUpType.Freeze:
                    OnPowerUpCollected?.Invoke();
                    break;
                case PowerUpType.SpeedBoost:
                    OnSpeedBoostCollected?.Invoke();
                    break;
            }

            Destroy(gameObject);
        }
    }
}

