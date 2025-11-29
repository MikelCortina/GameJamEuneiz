using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject player;
    public List<GameObject> listaObjetos;

    private int randomA;
    private int randomB;

    // Ya no necesitas GOPointA ni GOPointB como GameObjects en escena

    public void Randomizar()
    {
        // Elegimos dos índices aleatorios (pueden ser el mismo, si quieres evitarlo dime)
        randomA = Random.Range(0, listaObjetos.Count);
        randomB = Random.Range(0, listaObjetos.Count);

        Debug.Log("Spawn A en índice: " + randomA);
        Debug.Log("Spawn B en índice: " + randomB);

        // Posición base del jugador
        Vector3 posicionJugador = player.transform.position;

        // Posiciones relativas que antes ponías en GOPointA y GOPointB
        Vector3 posicionA = posicionJugador + new Vector3(43f, 1.5f, 0f);
        Vector3 posicionB = posicionJugador + new Vector3(43f, -1.5f, 0f);

        // Instanciamos directamente en esas posiciones calculadas
        if (listaObjetos[randomA] != null)
        {
            Instantiate(listaObjetos[randomA], posicionA, Quaternion.identity);
        }

        if (listaObjetos[randomB] != null)
        {
            Instantiate(listaObjetos[randomB], posicionB, Quaternion.identity);
        }
    }
}