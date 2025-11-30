using UnityEngine;

public class PanelSlideIntoCamera : MonoBehaviour
{
    [Header("Referencias")]
    [Tooltip("Transform de la cámara que seguirá el panel. Si está vacío usará Camera.main")]
    public Transform camara;

    [Header("Posiciones")]
    [Tooltip("Posición relativa inicial (fuera de la pantalla) respecto a la cámara")]
    public Vector3 offsetInicial = new Vector3(-600f, 0f, 0f);

    [Tooltip("Posición final respecto a la cámara una vez entra")]
    public Vector3 offsetObjetivo = new Vector3(0f, 0f, 0f);

    [Header("Movimiento")]
    [Tooltip("Velocidad de deslizamiento del panel")]
    public float velocidad = 5f;

    [Tooltip("Si está activo el panel se moverá, si se desactiva volverá a su posición inicial")]
    public bool mostrar = false;

    private Vector3 posicionObjetivo;

    void Start()
    {
        if (camara == null) camara = Camera.main.transform;

        // Al iniciar se coloca fuera de pantalla
        transform.position = camara.position + offsetInicial;
    }

    void Update()
    {
        posicionObjetivo = camara.position + (mostrar ? offsetObjetivo : offsetInicial);

        // Movimiento suave hacia la posición calculada
        transform.position = Vector3.Lerp(transform.position, posicionObjetivo, Time.deltaTime * velocidad);
    }
}
