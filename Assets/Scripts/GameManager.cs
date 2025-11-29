using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int decision;
    public AudioClip palancaSound;
    public AudioSource audioSource;
    public bool canChangeTrack = true;

    void Start()
    {
        // Asegurar que el audioSource tenga un clip asignado
        audioSource.clip = palancaSound;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && canChangeTrack) // solo al pulsar una vez
        {
            decision = 1;
            audioSource.Play(); // suena la palanca
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && canChangeTrack)
        {
            decision = 2;
            audioSource.Play(); // también puedes hacer sonar aquí si deseas
        }
    }
}
