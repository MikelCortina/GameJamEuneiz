using System.Collections.Generic;
using UnityEngine;

public class TileSpawner2D : MonoBehaviour
{
    [Header("Prefabs a usar como tile")]
    public GameObject[] tilePrefabs;

    [Header("Tamaño del pool por tipo de tile")]
    public int poolSizePerPrefab = 5;

    [Header("Spawn Settings")]
    public Transform cam;
    public float distanceToSpawn = 15f;
    public float startXPosition = 0f;

    private List<Queue<GameObject>> tilePools;          // Un pool por prefab
    private GameObject lastTile;
    private float lastTileRightX;
    private int nextTileIndex = 0;                      // Alternancia limpia

    void Start()
    {
        if (cam == null) cam = Camera.main.transform;
        if (tilePrefabs.Length == 0)
        {
            Debug.LogError("No hay tilePrefabs asignados al spawner");
            return;
        }

        CreatePools();
        lastTileRightX = startXPosition;

        // Evita popping cargando varios tiles iniciales
        for (int i = 0; i < 8; i++) SpawnTile();
    }

    void Update()
    {
        if (cam == null) return;

        // Si la cámara está a menos de X unidades del borde → generar otro tile
        if (lastTileRightX - cam.position.x < distanceToSpawn)
        {
            SpawnTile();
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

        float tileHalf = GetHalfWidth(tile);

        // primer tile
        if (lastTile == null)
        {
            tile.transform.position = new Vector3(startXPosition + tileHalf, transform.position.y, 0f);
        }
        else
        {
            float lastWidth = GetFullWidth(lastTile);
            tile.transform.position = lastTile.transform.position + Vector3.right * lastWidth;
        }

        lastTile = tile;
        lastTileRightX = tile.transform.position.x + tileHalf;

        nextTileIndex = (nextTileIndex + 1) % tilePrefabs.Length;
    }

    GameObject GetTileFromPool(int prefabIndex)
    {
        Queue<GameObject> pool = tilePools[prefabIndex];

        // Si se agota el pool, se expande automáticamente sin romper SOLID
        if (pool.Count == 0)
        {
            Debug.LogWarning("Pool agotado, expandiendo para evitar popping");
            GameObject extra = Instantiate(tilePrefabs[prefabIndex]);
            return extra;
        }

        GameObject tile = pool.Dequeue();

        // Al devolverlo se repondrá al final
        pool.Enqueue(tile);
        return tile;
    }

    float GetFullWidth(GameObject tile)
    {
        Renderer r = tile.GetComponentInChildren<Renderer>();
        return r ? r.bounds.size.x : 10f;      // fallback seguro
    }

    float GetHalfWidth(GameObject tile) => GetFullWidth(tile) * 0.5f;
}
