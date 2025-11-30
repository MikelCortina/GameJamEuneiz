using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public int decision = 0; // 0 = medio, 1 = arriba, 2 = abajo

    // Sonidos originales
    public AudioClip palancaArribaSound;
    public AudioClip palancaAbajoSound;
    public AudioClip palancaArribaMedioSound;
    public AudioClip palancaAbajoMedioSound;

    public AudioSource audioSource;
    public Animator animator;

    public int nuevaDecision;

    public bool canChangeTrack = true;
    public int decisionParaCuestas;

    // Input
    public InputActionAsset inputActions;
    private InputAction Arriba;
    private InputAction Abajo;

    public bool blocked;

    private void OnEnable()
    {
        if (inputActions == null)
        {
            Debug.LogWarning("InputActionAsset no asignado.");
            return;
        }


        Arriba = inputActions.FindAction("Player Controls/Up");
        Abajo = inputActions.FindAction("Player Controls/Down");

        Arriba?.Enable();
        Abajo?.Enable();
    }

    private void OnDisable()
    {
        Arriba?.Disable();
        Abajo?.Disable();
    }

    void Update()
    {
        nuevaDecision = decision;

        // Solo cambia cuando se PRESIONA (triggered), no al mantener ni al soltar
        if (Arriba.triggered && canChangeTrack&&!blocked)
        {
            nuevaDecision = 1; // Siempre a arriba al pulsar arriba
        }
        else if (Abajo.triggered && canChangeTrack && !blocked)
        {
            nuevaDecision = 2; // Siempre a abajo al pulsar abajo
        }
        

        // OPCIONAL: si quieres un botón para volver al centro (por ejemplo, ejemplo, el mismo botón de abajo dos veces o otro botón)
        // Descomenta esto si lo necesitas más adelante
        // else if (algúnOtroBotón.triggered)
        //     nuevaDecision = 0;

        // Si hay cambio de posición
        if (nuevaDecision != decision)
        {
            // === ANIMACIÓN DE TRANSICIÓN ===
            if (decision == 0 && nuevaDecision == 1)
                animator.Play("PalancaMedioArriba");
            else if (decision == 0 && nuevaDecision == 2)
                animator.Play("PalancaMedioAbajo");
            else if (decision == 1 && nuevaDecision == 2)
                animator.Play("PalancaArribaAbajo");
            else if (decision == 2 && nuevaDecision == 1)
                animator.Play("PalancaAbajoArriba");
            else if (decision == 1 && nuevaDecision == 0)
                animator.Play("PalancaArribaMedio");
            else if (decision == 2 && nuevaDecision == 0)
                animator.Play("PalancaAbajoMedio");

            // === SONIDOS ===
            if (nuevaDecision == 1)
                audioSource.PlayOneShot(palancaArribaSound);
            else if (nuevaDecision == 2)
                audioSource.PlayOneShot(palancaAbajoSound);
            else if (nuevaDecision == 0)
                audioSource.PlayOneShot(decision == 1 ? palancaArribaMedioSound : palancaAbajoMedioSound);

            // Actualizamos estado
            decision = nuevaDecision;
            decisionParaCuestas = decision;
        }
    }
}