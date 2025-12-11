using UnityEngine;
using System.Collections.Generic;

public class SpawnProbabilistico : MonoBehaviour
{
    public List<GameObject> objetos;
    public Transform puntoSpawn;

    void Start()
    {
        IntentarSpawn();
    }

    public void IntentarSpawn()
    {
        // Probabilidad de 1 entre 4
        int numero = Random.Range(0, 4);

        if (numero <= 1)
        {
            if (objetos.Count == 0)
            {
                Debug.LogWarning("La lista de objetos está vacía.");
                return;
            }

            int indice = Random.Range(0, objetos.Count);
            Instantiate(objetos[indice], puntoSpawn.position, puntoSpawn.rotation);
            Debug.Log("Objeto spawneado.");
        }
        else
        {
            Debug.Log("No se ha spawneado nada esta vez.");
        }
    }
}
