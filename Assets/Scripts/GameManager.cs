using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int decision;
    public AudioClip palancaArribaSound;
    public AudioClip palancaAbajoSound;
    public AudioClip palancaArribaMedioSound;
    public AudioClip palancaAbajoMedioSound;
    public AudioSource audioSource;
    public bool canChangeTrack = true;
    public int lastDecision = -1; // valor inicial que nunca será 0
    public int decisionParaCuestas;


    void Start()
    {
        // Asegurar que el audioSource tenga un clip asignado
      
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && canChangeTrack&&decision!=1) // solo al pulsar una vez
        {
          
            audioSource.clip = palancaArribaSound;
            decision = 1;
            decisionParaCuestas = decision;
            audioSource.Play(); // suena la palanca
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && canChangeTrack && decision != 2)
        {
        
            audioSource.clip = palancaAbajoSound;
            decision = 2;
            decisionParaCuestas = decision;
            audioSource.Play(); // también puedes hacer sonar aquí si deseas
        }

        
            // Si antes era 1 o 2, y ahora pasó a 0, entonces reproduce el sonido
            if ((lastDecision == 1 ) && decision == 0)
            {
         
            audioSource.clip = palancaArribaMedioSound;
                audioSource.Play();
            }
              // Si antes era 1 o 2, y ahora pasó a 0, entonces reproduce el sonido
            if ((lastDecision == 2) && decision == 0)
            {
            audioSource.clip = palancaAbajoMedioSound;
            audioSource.Play();
             }


        lastDecision = decision; // Actualizamos el valor anterior al final
        
    }
}
