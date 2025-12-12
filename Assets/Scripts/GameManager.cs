using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public int decision = 0; // 0 = medio, 1 = arriba, -1 = abajo

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
    private InputAction pausa;
    private bool pausado = false;

    public bool blocked;

    private Gamepad gamepad;



    private void OnEnable()
    {
        StopVibration();
    
        if (inputActions == null)
        {
            Debug.LogWarning("InputActionAsset no asignado.");
            return;
        }


        Arriba = inputActions.FindAction("Player Controls/Up");
        Abajo = inputActions.FindAction("Player Controls/Down");
        pausa = inputActions.FindAction("Player Controls/Pause");


        Arriba?.Enable();
        Abajo?.Enable();
        pausa?.Enable();
    }

    private void OnDisable()
    {
        StopVibration();
        Arriba?.Disable();
        Abajo?.Disable();
        pausa?.Disable();
    }

    void Start()
    {
      
        nuevaDecision = decision;
        Debug.Log(decision);
    }

    void Update()
    {
     
        if (canChangeTrack)
            nuevaDecision = decision;

        if (pausa.triggered && !pausado)
        {
            pausado = true;
        }else if(pausa.triggered && pausado)
        {
            pausado = false;
        }
        
        // Solo cambia cuando se PRESIONA (triggered), no al mantener ni al soltar
        if (Arriba.triggered && canChangeTrack&&!blocked && !pausado && nuevaDecision < 1)
        {
            nuevaDecision = decision + 1; // Siempre a arriba al pulsar arriba
            Debug.Log(nuevaDecision);
            var gamepad = Gamepad.current;
            if (gamepad != null)
            {
                // Iniciar la vibración del gamepad
                gamepad.SetMotorSpeeds(0.3f, 0.3f);
                Invoke("StopVibration", 0.1f); // Detener la vibración después de 0.2 segundos 
            }
        }
        else if (Abajo.triggered && canChangeTrack && !blocked && !pausado && nuevaDecision > -1)
        {
            nuevaDecision = decision - 1; // Siempre a abajo al pulsar abajo;
            Debug.Log(nuevaDecision);
            var gamepad = Gamepad.current;
            if (gamepad != null)
            {
                // Iniciar la vibración del gamepad
                gamepad.SetMotorSpeeds(0.3f, 0.3f);
                Invoke("StopVibration", 0.1f); // Detener la vibración después de 0.2 segundos 
            }
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
            else if (decision == 0 && nuevaDecision == -1)
                animator.Play("PalancaMedioAbajo");
            else if (decision == 1 && nuevaDecision == 0)
                animator.Play("PalancaArribaMedio");
            else if (decision == -1 && nuevaDecision == 0)
                animator.Play("PalancaAbajoMedio");

            // === SONIDOS ===
            if (nuevaDecision == 1)
                audioSource.PlayOneShot(palancaArribaSound);
            else if (nuevaDecision == -1)
                audioSource.PlayOneShot(palancaAbajoSound);
            else if (nuevaDecision == 0)
                audioSource.PlayOneShot(decision == 1 ? palancaArribaMedioSound : palancaAbajoMedioSound);

            // Actualizamos estado
            decision = nuevaDecision;
            decisionParaCuestas = decision;
        }
    }
    public void ForzarPalancaAlMedio()
    {
        if (decision == 1)
            animator.Play("PalancaArribaMedio");
        else if (decision == -1)
            animator.Play("PalancaAbajoMedio");

        // Sonido correcto al volver al centro
        audioSource.PlayOneShot(
            decision == 1 ? palancaArribaMedioSound : palancaAbajoMedioSound
        );

        decision = 0;
        nuevaDecision = 0;
        decisionParaCuestas = 0;

        Debug.Log("GameManager: palanca forzada al medio por trigger.");
    }

    private void StopVibration()
    {
        var pad = Gamepad.current;
        if (pad != null)
        {
            pad.SetMotorSpeeds(0f, 0f);
        }
    }
}