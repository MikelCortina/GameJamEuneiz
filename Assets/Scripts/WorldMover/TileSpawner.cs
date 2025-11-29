using System.Collections.Generic;
using UnityEngine;

public class TileSpawner2D : MonoBehaviour
{
    [Header("Pool Tiles")]
    public GameObject[] tilePrefabs;        // Ej: [0] = TileA, [1] = TileB
    public int poolSize = 20;

    [Header("Spawn Settings")]
    public Transform cam;
    public float distanceToSpawn = 15f;

    [Header("Punto de inicio en el mundo")]
    public float startXPosition = 0f;

    private Queue<GameObject> pool = new Queue<GameObject>();
    private GameObject lastTile;
    private int currentPrefabIndex = 0;          // Este controla la alternancia limpia
    private float lastTileRightX = 0f;

    void Start()
    {
        if (cam == null) cam = Camera.main.transform;
        if (tilePrefabs == null || tilePrefabs.Length == 0)
        {
            Debug.LogError("No hay tilePrefabs asignados!");
            return;
        }

        // === CREAR POOL LIMPIO ===
        for (int i = 0; i < poolSize; i++)
        {
            // Usamos siempre el prefab vacío o uno base si quieres, pero mejor uno "container"
            GameObject container = new GameObject("PooledTileContainer");
            container.SetActive(false);
            pool.Enqueue(container);
        }

        transform.position = new Vector3(startXPosition, transform.position.y, transform.position.z);
        lastTileRightX = startXPosition;

        SpawnInitialTile();
    }

    void Update()
    {
        if (lastTile == null) return;

        float distanceToRightEdge = cam.position.x - lastTileRightX;
        if (distanceToRightEdge > -distanceToSpawn)
        {
            SpawnTile();
        }
    }

    void SpawnInitialTile()
    {
        SpawnTile();
    }

    void SpawnTile()
    {
        if (pool.Count == 0)
        {
            Debug.LogWarning("Pool vacío! Aumenta poolSize.");
            return;
        }

        GameObject tileContainer = pool.Dequeue();
        tileContainer.SetActive(true);

        // === LIMPIAR HIJOS ANTERIORES (importante para evitar clones/solapes) ===
        foreach (Transform child in tileContainer.transform)
        {
            if (Application.isPlaying)
                Destroy(child.gameObject);
            else
                DestroyImmediate(child.gameObject);
        }

        // === INSTANCIAR EL NUEVO PREFAB DENTRO DEL CONTENEDOR ===
        GameObject newTile = Instantiate(tilePrefabs[currentPrefabIndex], tileContainer.transform);
        newTile.transform.localPosition = Vector3.zero;
        newTile.transform.localRotation = Quaternion.identity;

        // === POSICIONAR EL CONTENEDOR ===
        if (lastTile == null)
        {
            // Primer tile
            float halfWidth = GetTileHalfWidth(newTile);

            // Instanciamos o activamos el tile dentro del contenedor
            tileContainer.transform.position = new Vector3(
                startXPosition + halfWidth,
                transform.position.y,
                0f
            );
        }
        else
        {
            Vector3 offset = GetTileForwardOffset(lastTile);
            tileContainer.transform.position = lastTile.transform.position + offset;
        }

        // Actualizar borde derecho
        lastTileRightX = tileContainer.transform.position.x + GetTileHalfWidth(newTile);
        lastTile = tileContainer;

        // === AVANZAR AL SIGUIENTE PREFAB (alternancia perfecta) ===
        currentPrefabIndex = (currentPrefabIndex + 1) % tilePrefabs.Length;

        // Volver a encolar para reutilizar
        pool.Enqueue(tileContainer);
    }

    Vector3 GetTileForwardOffset(GameObject tileContainer)
    {
        return Vector3.right * GetTileFullWidth(tileContainer);
    }

    float GetTileFullWidth(GameObject tileContainer)
    {
        Renderer r = tileContainer.GetComponentInChildren<Renderer>();
        return r != null ? r.bounds.size.x : 10f;
    }

    float GetTileHalfWidth(GameObject tileContainer)
    {
        return GetTileFullWidth(tileContainer) * 0.5f;
    }
}