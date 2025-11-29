using System.Collections.Generic;
using UnityEngine;

public class TileSpawner2D : MonoBehaviour
{
    [Header("Pool Tiles")]
    public GameObject[] tilePrefabs; // Prefabs 2D (con SpriteRenderer y Collider2D)
    public int poolSize = 10;

    [Header("Spawn")]
    public Transform cam; // Cámara principal (debe moverse hacia la derecha)
    public float distanceToSpawn = 15f; // Distancia en X para spawnear el siguiente

    private Queue<GameObject> pool;
    private GameObject lastTile;
    private int prefabIndex = 0; // Control de alternancia

    void Start()
    {
        pool = new Queue<GameObject>();

        // Genera el pool alternando prefabs
        for (int i = 0; i < poolSize; i++)
        {
            GameObject t = Instantiate(tilePrefabs[prefabIndex]);
            prefabIndex = (prefabIndex + 1) % tilePrefabs.Length;
            t.SetActive(false);
            // Asegura posición 2D (Z=0)
            t.transform.position = new Vector3(0, 0, 0);
            t.transform.rotation = Quaternion.identity;
            pool.Enqueue(t);
        }
        prefabIndex = 0; // Reset para spawn real
        SpawnTile();
    }

    void Update()
    {
        if (lastTile == null) return;

        // Chequeo preciso en eje X (para 2D horizontal)
        if (Mathf.Abs(cam.position.x - lastTile.transform.position.x) < distanceToSpawn)
            SpawnTile();
    }

    void SpawnTile()
    {
        GameObject tile = pool.Dequeue();

        // Cambia el "modelo" (child) alternando prefabs (solo si >1 tipo)
        if (tilePrefabs.Length > 1)
        {
            // Destruye modelo anterior si existe
            if (tile.transform.childCount > 0)
                Destroy(tile.transform.GetChild(0).gameObject);

            // Instancia nuevo como child
            GameObject newModel = Instantiate(tilePrefabs[prefabIndex], tile.transform);
            prefabIndex = (prefabIndex + 1) % tilePrefabs.Length;

            // Asegura 2D en el child
            newModel.transform.localPosition = Vector3.zero;
            newModel.transform.localRotation = Quaternion.identity;
        }

        tile.SetActive(true);

        // Posición: siempre avanza en X positivo (hacia la derecha)
        if (lastTile == null)
            tile.transform.position = Vector3.zero;
        else
            tile.transform.position = lastTile.transform.position + GetTileForwardOffset(lastTile);

        // Mantiene Z=0 para 2D
        tile.transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y, 0);

        lastTile = tile;
        pool.Enqueue(tile);
    }

    // Offset SIEMPRE en X (derecha) para 2D endless runner horizontal
    Vector3 GetTileForwardOffset(GameObject tile)
    {
        Renderer r = tile.GetComponentInChildren<Renderer>(); // Funciona con SpriteRenderer
        if (r == null) return Vector3.right * 10f; // Fallback

        float tileWidth = r.bounds.size.x;
        return Vector3.right * tileWidth; // Avanza exactamente el ancho del tile
    }
}