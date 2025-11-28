using UnityEngine;

public class MoveRight : MonoBehaviour
{
    [Tooltip("Velocidad de movimiento en unidades por segundo")]
    public float speed = 5f;

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
}
