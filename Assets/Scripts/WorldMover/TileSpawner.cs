using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    [Header("Pool Tiles")]
    public GameObject[] tilePrefabs;
    public int poolSize = 10;

    [Header("Spawn")]
    public Transform cam;
    public float distanceToSpawn = 30f;

    private Queue<GameObject> pool;
    private GameObject lastTile;

    private int prefabIndex = 0; // Control de alternancia

    void Start()
    {
        pool = new Queue<GameObject>();

        // Se generará el pool alternando prefabs
        for (int i = 0; i < poolSize; i++)
        {
            GameObject t = Instantiate(tilePrefabs[prefabIndex]);
            prefabIndex = (prefabIndex + 1) % tilePrefabs.Length;

            t.SetActive(false);
            pool.Enqueue(t);
        }

        prefabIndex = 0; // reseteamos para uso real en SpawnTile
        SpawnTile();
    }

    void Update()
    {
        if (lastTile == null) return;

        if (Vector3.Distance(cam.position, lastTile.transform.position) < distanceToSpawn)
            SpawnTile();
    }

    void SpawnTile()
    {
        GameObject tile = pool.Dequeue();

        // Cambiar el mesh del objeto añadiendo el siguiente prefab
        // solo si hay más de un tipo disponible
        if (tilePrefabs.Length > 1)
        {
            GameObject newModel = Instantiate(tilePrefabs[prefabIndex], tile.transform);
            prefabIndex = (prefabIndex + 1) % tilePrefabs.Length;

            // Destruye el modelo anterior si existe
            if (tile.transform.childCount > 1)
                Destroy(tile.transform.GetChild(0).gameObject);
        }

        tile.SetActive(true);

        if (lastTile == null)
            tile.transform.position = Vector3.zero;
        else
            tile.transform.position = lastTile.transform.position + GetTileForwardOffset(lastTile);

        lastTile = tile;
        pool.Enqueue(tile);
    }

    Vector3 GetTileForwardOffset(GameObject tile)
    {
        Renderer r = tile.GetComponentInChildren<Renderer>();
        Vector3 size = r.bounds.size;

        if (size.z >= size.x && size.z >= size.y) return new Vector3(0, 0, size.z);
        if (size.x >= size.y) return new Vector3(size.x, 0, 0);
        return new Vector3(0, size.y, 0);
    }
}
