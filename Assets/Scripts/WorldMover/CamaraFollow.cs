using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    public Transform target;

    [Header("Offset final (posición normal de seguimiento)")]
    public Vector2 followOffset = new Vector2(0, 5);

    [Header("Offset inicial (dónde empieza la cámara)")]
    public Vector2 startOffset = new Vector2(-25, 10);

    [Header("Duración de la intro (en segundos)")]
    public float introDuration = 3.5f;

    [Header("Suavizado al seguir al tren (0 = pegada, 0.1-0.2 recomendado)")]
    [Range(0f, 1f)] public float followSmooth = 0.12f;

    public float introTimer = 0f;
    public bool introFinished = false;

    private float currentX, currentY;

    void Start()
    {
        if (target == null)
        {
            Debug.LogError("CameraFollow: ¡Falta asignar el target!");
            enabled = false;
            return;
        }

        // Posición inicial
        currentX = target.position.x + startOffset.x;
        currentY = target.position.y + startOffset.y;
        transform.position = new Vector3(currentX, currentY, transform.position.z);
    }

    void LateUpdate()
    {
        if (target == null) return;

        if (!introFinished)
        {
            introTimer += Time.deltaTime;
            float t = introTimer / introDuration;

            if (t >= 1f)
            {
                t = 1f;
                introFinished = true;
            }

            // Esta es la magia: una aceleración natural y perfecta
            float progress = t * t * (3f - 2f * t); // ← SmoothStep (empieza lento, acaba rápido y suave)

            currentX = Mathf.Lerp(target.position.x + startOffset.x,
                                  target.position.x + followOffset.x, progress);

            currentY = Mathf.Lerp(target.position.y + startOffset.y,
                                  target.position.y + followOffset.y, progress);
        }
        else
        {
            // Seguimiento normal con un poquito de suavizado (se siente premium)
            float targetX = target.position.x + followOffset.x;
            float targetY = target.position.y + followOffset.y;

            currentX = Mathf.Lerp(currentX, targetX, 1f - followSmooth);
            currentY = Mathf.Lerp(currentY, targetY, 1f - followSmooth);
        }

        // Z nunca se toca → perfecto para 2.5D
        transform.position = new Vector3(currentX, currentY, transform.position.z);
    }
}