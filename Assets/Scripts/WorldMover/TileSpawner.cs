using System.Collections.Generic;
using UnityEngine;

public class TileSpawner2D : MonoBehaviour
{
    [Header("Prefabs a usar como tile")]
    public GameObject[] tilePrefabs;

    [Header("Tamaño del pool por tipo de tile")]
    public int poolSizePerPrefab = 10;

    [Header("Spawn Settings")]
    public Transform cam;
    public float spawnDistanceAhead = 20f;      // Distancia delante de la cámara para spawnear
    public float despawnDistanceBehind = 30f;   // Distancia detrás para desactivar
    public float startXPosition = 0f;

    private List<Queue<GameObject>> tilePools;
    private List<GameObject> activeTiles = new List<GameObject>(); // ¡¡NUEVO!!
    private float lastTileRightX;
    private int nextTileIndex = 0;

    void Start()
    {
        if (cam == null) cam = Camera.main.transform;
        if (tilePrefabs.Length == 0)
        {
            Debug.LogError("No hay tilePrefabs asignados");
            return;
        }

        CreatePools();
        lastTileRightX = startXPosition;

        // Precarga inicial
        for (int i = 0; i < 10; i++)
            SpawnTile();
    }

    void Update()
    {
        if (cam == null) return;

        // Spawnear si el último tile está cerca
        float cameraRightEdge = cam.position.x + spawnDistanceAhead;
        while (lastTileRightX < cameraRightEdge)
        {
            SpawnTile();
        }

        // Desactivar tiles que quedaron muy atrás (¡CRUCIAL!)
        float cameraLeftEdge = cam.position.x - despawnDistanceBehind;
        for (int i = activeTiles.Count - 1; i >= 0; i--)
        {
            GameObject tile = activeTiles[i];
            if (tile.transform.position.x + GetHalfWidth(tile) < cameraLeftEdge)
            {
                ReturnTileToPool(tile);
                activeTiles.RemoveAt(i);
            }
        }
    }

    void CreatePools()
    {
        tilePools = new List<Queue<GameObject>>();
        foreach (GameObject prefab in tilePrefabs)
        {
            Queue<GameObject> pool = new Queue<GameObject>();
            for (int i = 0; i < poolSizePerPrefab; i++)
            {
                GameObject obj = Instantiate(prefab);
                obj.SetActive(false);
                obj.transform.parent = transform; // opcional: organizar jerarquía
                pool.Enqueue(obj);
            }
            tilePools.Add(pool);
        }
    }

    void SpawnTile()
    {
        GameObject tile = GetTileFromPool(nextTileIndex);
        if (tile == null) return;

        tile.SetActive(true);
        activeTiles.Add(tile); // ¡Importante!

        float tileHalfWidth = GetHalfWidth(tile);

        if (activeTiles.Count == 1)
        {
            // Primer tile
            tile.transform.position = new Vector3(startXPosition + tileHalfWidth, transform.position.y, 0f);
        }
        else
        {
            // Colocar al final del último tile activo
            GameObject last = activeTiles[activeTiles.Count - 2];
            float lastWidth = GetFullWidth(last);
            tile.transform.position = last.transform.position + Vector3.right * lastWidth;
        }

        lastTileRightX = tile.transform.position.x + tileHalfWidth;
        nextTileIndex = (nextTileIndex + 1) % tilePrefabs.Length;
    }

    GameObject GetTileFromPool(int prefabIndex)
    {
        Queue<GameObject> pool = tilePools[prefabIndex];

        GameObject tile;
        if (pool.Count == 0)
        {
            Debug.LogWarning($"Pool agotado para prefab {prefabIndex}, creando extra");
            tile = Instantiate(tilePrefabs[prefabIndex]);
            tile.transform.parent = transform;
        }
        else
        {
            tile = pool.Dequeue();
        }

        // Siempre re-enqueue al final (para rotación)
        pool.Enqueue(tile);
        return tile;
    }

    void ReturnTileToPool(GameObject tile)
    {
        tile.SetActive(false);
        // Opcional: resetear posición, rotación, etc.
        tile.transform.position = new Vector3(0, -1000, 0); // fuera de vista
    }

    float GetFullWidth(GameObject tile)
    {
        Renderer r = tile.GetComponentInChildren<Renderer>();
        if (r != null) return r.bounds.size.x;

        // Fallback: intentar con Collider2D
        Collider2D col = tile.GetComponentInChildren<Collider2D>();
        if (col != null) return col.bounds.size.x;

        return 10f;
    }

    float GetHalfWidth(GameObject tile) => GetFullWidth(tile) * 0.5f;
}