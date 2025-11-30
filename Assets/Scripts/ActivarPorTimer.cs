using UnityEngine;

public class ActivarPorTiempo : MonoBehaviour
{
    [SerializeField] private GameObject objetoAActivar; // El objeto que se activará
    [SerializeField] private float tiempoDeEspera = 5f; // Tiempo antes de activarlo

    private float contador;

    void Update()
    {
        contador += Time.deltaTime;

        if (contador >= tiempoDeEspera)
        {
            objetoAActivar.SetActive(true);
            enabled = false; // Desactiva este script para no seguir comprobando
        }
    }
}

