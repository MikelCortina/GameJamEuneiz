using UnityEngine;

public class VagonSeguidor : MonoBehaviour
{
    [SerializeField] private Transform locomotora;
    [SerializeField] private SpriteRenderer miRenderer;
    [SerializeField] private float distanciaX = 2f;

    [Header("Delay y Suavizado")]
    [SerializeField] private float delayY = 0.1f;      // retardo
    [SerializeField] private float smoothY = 8f;       // suavidad del movimiento

    private float delayedY;
    private float timer;

    void Start()
    {
        delayedY = locomotora.position.y;
    }

    void LateUpdate()
    {
        // eje Y con retardo y suavizado
        timer += Time.deltaTime;
        if (timer >= delayY)
        {
            delayedY = locomotora.position.y;
            timer = 0f;
        }

        // interpolación ? elimina los saltos
        float yLerp = Mathf.Lerp(transform.position.y, delayedY, Time.deltaTime * smoothY);

        // X sigue inmediato
        float xPos = locomotora.position.x - distanciaX;

        transform.position = new Vector3(xPos, yLerp, transform.position.z);

    }
}
