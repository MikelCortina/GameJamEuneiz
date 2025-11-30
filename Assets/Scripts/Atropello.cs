using UnityEngine;
using UnityEngine.InputSystem;

public class Atropello : MonoBehaviour
{
    public EntidadData entidadData;

    private Gamepad gamepad;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // el tren
        {
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
