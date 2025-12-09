using UnityEngine;

public class MoverObjetoPorTrigger : MonoBehaviour
{
    [Header("Objeto que se va a mover")]
    [Tooltip("Arrastra aquí el GameObject que quieres que se mueva")]
    public Transform objetoAMover;

    [Header("Opciones")]
    public bool moverSoloUnaVez = true;        // Si quieres que solo se mueva la primera vez
    public bool mantenerOffsetX = true;        // Mantener la posición X original del objeto a mover
    public bool mantenerOffsetZ = true;        // Mantener la posición Z original (útil en 2D suele ser 0)

    private bool yaMovio = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Opcional: puedes filtrar por tag, layer, etc.
        // if (!collision.CompareTag("Player")) return;

        if (moverSoloUnaVez && yaMovio) return;

        if (objetoAMover == null)
        {
            Debug.LogError("¡Asigna un objeto a mover en el Inspector!", this);
            return;
        }

        // Calculamos la nueva posición
        Vector3 nuevaPosicion = objetoAMover.position;

        // Siempre copiamos la Y del objeto que tiene este script
        nuevaPosicion.y = transform.position.y;

        // Opcional: mantener X y/o Z original del objeto a mover
        if (mantenerOffsetX)
            nuevaPosicion.x = objetoAMover.position.x;

        if (mantenerOffsetZ)
            nuevaPosicion.z = objetoAMover.position.z;

        // Movemos el objeto
        objetoAMover.position = nuevaPosicion;

        yaMovio = true;

        Debug.Log($"Objeto movido a Y = {nuevaPosicion.y}");
    }

    // Reinicia si sales del trigger y quieres permitir mover otra vez
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!moverSoloUnaVez)
            yaMovio = false;
    }
}