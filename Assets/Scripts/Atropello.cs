using UnityEngine;

public class Atropello : MonoBehaviour
{
    public EntidadData entidadData;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // el tren
        {
        Debug.Log("PEIO");
            if (entidadData != null)
            {
                entidadData.Morir();
            }
        }
    }
}
