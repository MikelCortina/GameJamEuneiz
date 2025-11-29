using System.Collections.Generic;
using UnityEngine;

public class OrderedObjectSpawner : MonoBehaviour
{
    [Header("Pool Settings")]
    public List<GameObject> prefabs; // Lista de prefabs a instanciar en orden
    public int poolSize = 10; // Tamaño del pool (debe ser >= firstPrefabRepeat + resto deseado)
    public float spawnDistance = 5f; // Distancia entre objetos
    public int firstPrefabRepeat = 3; // Cuántas veces se repite el primer prefab al inicio

    [Header("Spawn Trigger")]
    public Transform cameraTransform;
    public float triggerX = 10f;

    private Queue<GameObject> pool = new Queue<GameObject>(); // Cambiamos a Queue para mejor control
    private int spawnedCount = 0; // Lleva la cuenta de cuántos objetos se han creado en total
    private Vector3 nextSpawnPosition = Vector3.zero;

    // Índices para controlar el flujo
    private int initialRepeatsDone = 0; // Cuántas veces ya se ha usado el primer prefab
    private int currentPrefabIndex = 1; // Empezamos desde el segundo prefab después de las repeticiones

    void Start()
    {
        if (prefabs == null || prefabs.Count == 0)
        {
            Debug.LogError("La lista de prefabs está vacía!");
            return;
        }

        // Prellenamos el pool con: firstPrefabRepeat veces el primero + el resto cíclico
        for (int i = 0; i < poolSize; i++)
        {
            GameObject prefabToSpawn;

            if (initialRepeatsDone < firstPrefabRepeat)
            {
                prefabToSpawn = prefabs[0];
                initialRepeatsDone++;
            }
            else
            {
                // Después de repetir el primero, seguimos con el resto en orden cíclico (excluyendo el índice 0)
                prefabToSpawn = prefabs[currentPrefabIndex];
                currentPrefabIndex = currentPrefabIndex + 1;
                if (currentPrefabIndex >= prefabs.Count)
                    currentPrefabIndex = 1; // Saltamos el 0, volvemos al 1
            }

            GameObject obj = Instantiate(prefabToSpawn, nextSpawnPosition, Quaternion.identity);
            obj.SetActive(true);
            pool.Enqueue(obj);

            nextSpawnPosition += Vector3.right * spawnDistance;
            spawnedCount++;
        }
    }

    void Update()
    {
        if (cameraTransform.position.x >= triggerX)
        {
            SpawnNext();
            triggerX += spawnDistance;
        }
    }

    void SpawnNext()
    {
        // Reutilizamos el objeto más antiguo (el primero que entró al pool)
        GameObject obj = pool.Dequeue();

        // Determinamos cuál prefab usar para el nuevo spawn
        GameObject prefabToUse;

        if (spawnedCount < firstPrefabRepeat)
        {
            // Esto solo pasa en los primeros spawns (por seguridad, aunque ya no debería entrar aquí)
            prefabToUse = prefabs[0];
        }
        else
        {
            // Ciclo normal: todos los prefabs EXCEPTUANDO el primero
            prefabToUse = prefabs[currentPrefabIndex];
            currentPrefabIndex = currentPrefabIndex + 1;
            if (currentPrefabIndex >= prefabs.Count)
                currentPrefabIndex = 1; // Salta el índice 0
        }

        // Actualizamos el prefab del objeto reutilizado (importante si tiene componentes únicos)
        // Si tus objetos tienen lógica interna que depende del prefab, mejor destruir y recrear.
        // Pero para eficiencia, asumimos que puedes reutilizar cambiando solo el modelo o sprite.
        // Si necesitas instanciar uno nuevo cada vez, cambia esta parte.

        // Opción segura y limpia: destruir y crear uno nuevo (recomendado si hay diferencias grandes)
        Destroy(obj);
        GameObject newObj = Instantiate(prefabToUse, nextSpawnPosition, Quaternion.identity);
        pool.Enqueue(newObj);

        nextSpawnPosition += Vector3.right * spawnDistance;
        spawnedCount++;
    }
}