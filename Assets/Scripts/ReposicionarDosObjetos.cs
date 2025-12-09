using UnityEngine;

public class ReposicionarDosObjetos : MonoBehaviour
{
    [Header("Objetos que se van a mover")]
    public Transform objetoA;
    public Transform objetoB;

    [Header("Las tres posiciones posibles")]
    public Transform posicion1;
    public Transform posicion2;
    public Transform posicion3;

    void Awake()
    {
        // Guardamos las tres posiciones en un array para manejarlo más fácil
        Transform[] posiciones = new Transform[] { posicion1, posicion2, posicion3 };

        // Mezclamos el array (Fisher-Yates shuffle simple)
        for (int i = posiciones.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            Transform temp = posiciones[i];
            posiciones[i] = posiciones[j];
            posiciones[j] = temp;
        }

        // Asignamos las primeras dos posiciones del array mezclado a los objetos
        // Así nunca van a coincidir porque toman índices distintos
        objetoA.position = posiciones[0].position;
        objetoA.rotation = posiciones[0].rotation;

        objetoB.position = posiciones[1].position;
        objetoB.rotation = posiciones[1].rotation;

        // La tercera posición queda libre automáticamente
    }
}