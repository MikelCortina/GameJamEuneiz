using UnityEngine;
using UnityEngine.InputSystem;

public class Atropello : MonoBehaviour
{
    public EntidadData entidadData;

    private Gamepad gamepad;

    public Animator animator;
    public ShakePanel shakePanel; // Referencia al script ShakePanel

    void Start()
    {
        // Busca el objeto que tenga el componente ShakePanel
        GameObject shakeObj = GameObject.Find("Rayo");
        if (shakeObj != null)
            shakePanel = shakeObj.GetComponent<ShakePanel>();

        // Busca el objeto que tenga el componente Animator
        GameObject animatorObj = GameObject.Find("Maquin2_0");
        if (animatorObj != null)
            animator = animatorObj.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // el tren
        {
            if (shakePanel != null)
                shakePanel.Shake();

            if (animator != null)
                animator.Play("SustoClip");

            Debug.Log("PEIO");
            if (entidadData != null)
            {
                entidadData.Morir();
                var gamepad = Gamepad.current;
                if (gamepad != null)
                {
                    // Iniciar la vibración del gamepad
                    gamepad.SetMotorSpeeds(1f, 1f);
                    Invoke("StopVibration", 0.2f); // Detener la vibración después de 0.2 segundos 
                }
                
            }
        }
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
