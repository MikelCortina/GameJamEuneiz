using UnityEngine;

public class MovimientoDerecha : MonoBehaviour
{
    // Velocidad de movimiento
    public float velocidad = 5f;

    // Variable de control
    public bool mover = true;

    void Update()
    {
        if (mover)
        {
            // Mueve el objeto hacia la derecha
            transform.Translate(Vector3.right * velocidad * Time.deltaTime);
        }
    }
}
