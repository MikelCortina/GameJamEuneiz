using System.Collections.Generic;
using UnityEngine;

public class OrderedObjectSpawner : MonoBehaviour
{
    [Header("Pool Settings")]
    public List<GameObject> prefabs;
    public int poolSize = 20;
    public float spawnDistance = 5f;
    public int firstPrefabRepeat = 3;

    [Header("Punto de inicio")]
    [Tooltip("Coordenada X desde donde empezarán a spawnearse los objetos")]
    public float startXPosition = 0f;   // <<<< CAMBIA AQUÍ (ej: 1000f)

    [Header("Spawn Trigger")]
    public Transform cameraTransform;
    public float initialTriggerOffset = 10f; // Cuánto delante de startX empieza el trigger

    private Queue<GameObject> pool = new Queue<GameObject>();
    private int spawnedCount = 0;
    private Vector3 nextSpawnPosition;
    private float triggerX;

    // Control de orden
    private int initialRepeatsDone = 0;
    private int currentPrefabIndex = 1;

    void Start()
    {
        if (prefabs == null || prefabs.Count == 0)
        {
            Debug.LogError("Lista de prefabs vacía!");
            return;
        }
        if (cameraTransform == null) cameraTransform = Camera.main.transform;

        // Posición inicial de spawn
        nextSpawnPosition = new Vector3(startXPosition, transform.position.y, 0f);
        triggerX = startXPosition + initialTriggerOffset;

        // Pre-llenar el pool desde startXPosition
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(GetPrefabToUse(), nextSpawnPosition, Quaternion.identity);
            obj.SetActive(true);
            pool.Enqueue(obj);

            nextSpawnPosition += Vector3.right * spawnDistance;
            spawnedCount++;
        }

        // Ajustar trigger después del preload
        triggerX = startXPosition + initialTriggerOffset;
    }

    void Update()
    {
        if (cameraTransform.position.x >= triggerX)
        {
            SpawnNext();
            triggerX += spawnDistance;
        }
    }

    GameObject GetPrefabToUse()
    {
        if (spawnedCount < firstPrefabRepeat)
        {
            initialRepeatsDone++;
            return prefabs[0];
        }
        else
        {
            GameObject prefab = prefabs[currentPrefabIndex];
            currentPrefabIndex++;
            if (currentPrefabIndex >= prefabs.Count)
                currentPrefabIndex = 1; // saltamos el 0
            return prefab;
        }
    }

    void SpawnNext()
    {
        GameObject oldObj = pool.Dequeue();
        Destroy(oldObj); // Recomendado si los objetos tienen lógica diferente

        GameObject newObj = Instantiate(GetPrefabToUse(), nextSpawnPosition, Quaternion.identity);
        pool.Enqueue(newObj);

        nextSpawnPosition += Vector3.right * spawnDistance;
        spawnedCount++;
    }
}