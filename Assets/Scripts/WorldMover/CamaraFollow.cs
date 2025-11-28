using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;               // El objeto a seguir
    public Vector3 offset = new Vector3(0, 5, -10); // Separación de la cámara respecto al objetivo

    void LateUpdate()
    {
        if (target == null) return;

        transform.position = target.position + offset;
    }
}
