using System.Collections.Generic;
using UnityEngine;
public class SpawnManager : MonoBehaviour
{
    public int randomA;
    public int randomB;
    public GameObject GOPointA;
    public GameObject GOPointB;
    public List<GameObject> listaObjetos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Randomizar();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Randomizar()
    {
        randomA = Random.Range(0, listaObjetos.Count);
        Debug.Log("A: " + randomA);
        randomB = Random.Range(0, listaObjetos.Count);
        Debug.Log("B: " + randomB);

        // Instanciar GameObjects en la posición deseada
        if (listaObjetos[randomA] != null && GOPointA.transform.position != null)
            Instantiate(listaObjetos[randomA], GOPointA.transform.position, Quaternion.identity);

        if (listaObjetos[randomB] != null && GOPointB.transform.position != null)
            Instantiate(listaObjetos[randomB], GOPointB.transform.position, Quaternion.identity);

        //CAMBIO DE PUNTOS
            GOPointA.transform.position += new Vector3(40,0,0);
            GOPointB.transform.position += new Vector3(40, 0, 0);
    }
}
