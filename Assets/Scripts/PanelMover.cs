using UnityEngine;
using UnityEngine.UI;

public class PanelMover : MonoBehaviour
{
    [Header("Referencia al Panel")]
    public RectTransform panel;

    [Header("Configuración de Movimiento")]
    public Vector2 posicionInicial;
    public Vector2 posicionFinal;
    public float duracion = 1.5f;

    private float tiempoTranscurrido = 0f;
    private bool moviendo = false;

    public IntroLetterbox letterbox; // referencia necesaria


    private void Start()
    {
        // Colocamos el panel en la primera posición
        panel.anchoredPosition = posicionInicial;
        moviendo = true;
    }

    private void Update()
    {
        if (!moviendo) return;

        tiempoTranscurrido += Time.deltaTime;
        float t = tiempoTranscurrido / duracion;

        // Interpolamos la posición
        panel.anchoredPosition = Vector2.Lerp(posicionInicial, posicionFinal, t);

        // Detenemos cuando termina
        if (t >= 1f)
            moviendo = false;

        if (moviendo == false)
        {
            letterbox.CerrarBandas(); // Llama al método para abrir las bandas negras
        }
    }
}
