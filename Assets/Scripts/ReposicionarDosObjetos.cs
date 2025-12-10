using UnityEngine;

public class ReposicionarDosObjetos : MonoBehaviour
{
    [Header("Objetos que se van a mover")]
    public Transform objetoA;
    public Transform objetoB;
    public Transform objetoC;

    [Header("Las tres posiciones posibles")]
    [Header("Posiciones posibles")]
    public Transform[] posiciones; // Array de Transform
    public Transform[] posiciones2; // Array de Transform

    void Awake()
    {

        // Mezclamos el array (Fisher-Yates shuffle simple)
        for (int i = posiciones.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            Transform temp = posiciones[i];
            posiciones[i] = posiciones[j];
            posiciones[j] = temp;
        }
        for (int i = posiciones2.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            Transform temp = posiciones2[i];
            posiciones2[i] = posiciones2[j];
            posiciones2[j] = temp;
        }

        // Asignamos las primeras dos posiciones del array mezclado a los objetos
        // Así nunca van a coincidir porque toman índices distintos
        objetoB.position = posiciones[0].position;
        objetoB.rotation = posiciones[0].rotation;
        objetoC.position = posiciones[0].position;
        objetoC.rotation = posiciones[0].rotation;


        objetoA.position = posiciones2[1].position;
        objetoA.rotation = posiciones2[1].rotation;

        // La tercera posición queda libre automáticamente
    }
}