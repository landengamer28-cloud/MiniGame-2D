using UnityEngine;
using System.Collections;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public GameObject powerUpPrefab;  // Asigna tu prefab aquí
    private GameObject currentPowerUp;
    public float respawnDelay = 15f;   // Tiempo de espera en segundos antes de respawnear

    void Start()
    {
        SpawnPowerUp();
    }

    void SpawnPowerUp()
    {
        // Si ya hay un PowerUp activo, no crear otro
        if (currentPowerUp != null) return;

        // Obtener límites de la cámara en coordenadas del mundo
        Vector3 minPantalla = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 10f));
        Vector3 maxPantalla = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 10f));

        // Calcular posición aleatoria dentro de los límites
        float randomX = Random.Range(minPantalla.x, maxPantalla.x);
        float randomY = Random.Range(minPantalla.y, maxPantalla.y);
        Vector3 spawnPosition = new Vector3(randomX, randomY, 0f);

        // Instanciar el PowerUp
        currentPowerUp = Instantiate(powerUpPrefab, spawnPosition, Quaternion.identity);

        // Registrar evento cuando se destruya
        PowerUp powerUpScript = currentPowerUp.GetComponent<PowerUp>();
        if (powerUpScript != null)
        {
            powerUpScript.OnCollected += HandlePowerUpCollected;
        }
    }

    void HandlePowerUpCollected()
    {
        // Quitar referencia
        currentPowerUp = null;
        // Esperar antes de crear uno nuevo
        StartCoroutine(RespawnAfterDelay());
    }

    // Coroutine para esperar y respawnear
    IEnumerator RespawnAfterDelay()
    {
        yield return new WaitForSeconds(respawnDelay);
        SpawnPowerUp();
    }
}
