using System.Collections.Generic;
using UnityEngine;

public class OrderedObjectSpawner : MonoBehaviour
{
    [Header("Pool Settings")]
    public List<GameObject> prefabs;      // Lista de prefabs a instanciar en orden
    public int poolSize = 10;             // Tamaño del pool
    public float spawnDistance = 5f;      // Distancia entre objetos

    [Header("Spawn Trigger")]
    public Transform cameraTransform;     // Cámara de referencia
    public float triggerX = 10f;          // X relativo de la cámara para activar spawn adicional (si quieres)

    private List<GameObject> pool = new List<GameObject>();
    private int poolIndex = 0;            // Índice dentro del pool
    private int prefabIndex = 0;          // Índice del prefab a instanciar
    private Vector3 nextSpawnPosition = Vector3.zero;

    void Start()
    {
        // Instanciar todo el pool al inicio, en orden y distribuido hacia delante
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefabs[prefabIndex], nextSpawnPosition, Quaternion.identity);
            obj.SetActive(true);
            pool.Add(obj);

            // Preparar la posición del siguiente objeto
            nextSpawnPosition += Vector3.right * spawnDistance;

            // Avanzar al siguiente prefab
            prefabIndex = (prefabIndex + 1) % prefabs.Count;
        }
    }

    void Update()
    {
        // Si quieres seguir instanciando objetos según la cámara, puedes dejar este bloque
        if (cameraTransform.position.x >= triggerX)
        {
            SpawnNext();
            triggerX += spawnDistance;
        }
    }

    void SpawnNext()
    {
        GameObject obj = pool[poolIndex];
        obj.transform.position = nextSpawnPosition;
        obj.SetActive(true);

        // Preparar la posición para el siguiente spawn
        nextSpawnPosition += Vector3.right * spawnDistance;

        // Avanzar en el pool
        poolIndex = (poolIndex + 1) % pool.Count;
    }
}
