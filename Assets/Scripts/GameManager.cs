using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int decision;
    public AudioClip palancaSound;
    public AudioClip palancaReturnSound;
    public AudioSource audioSource;
    public bool canChangeTrack = true;
    int lastDecision = -1; // valor inicial que nunca será 0


    void Start()
    {
        // Asegurar que el audioSource tenga un clip asignado
      
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && canChangeTrack) // solo al pulsar una vez
        {
            audioSource.volume = 1f; // 0 = silencio, 1 = volumen completo
            audioSource.clip = palancaSound;
            decision = 1;
            audioSource.Play(); // suena la palanca
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && canChangeTrack)
        {
            audioSource.volume = 1f; // 0 = silencio, 1 = volumen completo
            audioSource.clip = palancaSound;
            decision = 2;
            audioSource.Play(); // también puedes hacer sonar aquí si deseas
        }

        
            // Si antes era 1 o 2, y ahora pasó a 0, entonces reproduce el sonido
            if ((lastDecision == 1 || lastDecision == 2) && decision == 0)
            {
            audioSource.volume = 0.5f; // 0 = silencio, 1 = volumen completo
            audioSource.clip = palancaReturnSound;
                audioSource.Play();
            }

            lastDecision = decision; // Actualizamos el valor anterior al final
        
    }
}
