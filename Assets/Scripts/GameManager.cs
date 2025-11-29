using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public int decision;
    public AudioClip palancaArribaSound;
    public AudioClip palancaAbajoSound;
    public AudioClip palancaArribaMedioSound;
    public AudioClip palancaAbajoMedioSound;
    public AudioSource audioSource;
    public bool canChangeTrack = true;
    public int lastDecision = -1; // valor inicial que nunca ser� 0
    public int decisionParaCuestas;

    public InputActionAsset inputActions; // Referencia al asset input actions 

    private InputAction Arriba;
    private InputAction Abajo;

    private void OnEnable()
    {
        if (inputActions == null)
        {
            Debug.LogWarning("InputActionAsset no asignado en el inspector.");
            return;
        }

        // Buscar las acciones por ruta (ajusta el nombre del action map / acción si es distinto)
        Arriba = inputActions.FindAction("Player Controls/Up");
        Abajo  = inputActions.FindAction("Player Controls/Down");

        // Enable the actions
        Arriba.Enable();
        Abajo.Enable();        
    }

    private void OnDisable()
    {
        // Disable the actions
        Arriba.Disable();
        Abajo.Disable();
    }

    void Start()
    {
        // Asegurar que el audioSource tenga un clip asignado
      
    }

    void Update()
    {
        if (Arriba.triggered && canChangeTrack&&decision!=1) // solo al pulsar una vez
        {
          
            audioSource.clip = palancaArribaSound;
            decision = 1;
            decisionParaCuestas = decision;
            audioSource.Play(); // suena la palanca
        }

        if (Abajo.triggered && canChangeTrack && decision != 2)
        {
        
            audioSource.clip = palancaAbajoSound;
            decision = 2;
            decisionParaCuestas = decision;
            audioSource.Play(); // tambi�n puedes hacer sonar aqu� si deseas
        }

        
            // Si antes era 1 o 2, y ahora pas� a 0, entonces reproduce el sonido
            if ((lastDecision == 1 ) && decision == 0)
            {
         
            audioSource.clip = palancaArribaMedioSound;
                audioSource.Play();
            }
              // Si antes era 1 o 2, y ahora pas� a 0, entonces reproduce el sonido
            if ((lastDecision == 2) && decision == 0)
            {
            audioSource.clip = palancaAbajoMedioSound;
            audioSource.Play();
             }


        lastDecision = decision; // Actualizamos el valor anterior al final
        
    }
}
