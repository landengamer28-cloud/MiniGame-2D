using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMusicController : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true; // Asegura que se repita
        audioSource.Play();
    }

    public void OnStartButton()
    {
        audioSource.Stop(); // Detiene la música
     
    }
}
