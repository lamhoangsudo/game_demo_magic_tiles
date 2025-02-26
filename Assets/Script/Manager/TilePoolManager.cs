using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class TilePoolManager : MonoBehaviour
{
    public static TilePoolManager instance;
    [SerializeField] private TileDataSO tile;
    [SerializeField] private int initialPoolSize = 10;
    [SerializeField] private int maxPoolSize = 20;
    private ObjectPool<GameObject> tilePool;
    private HashSet<GameObject> activeTile = new();

    private void Awake()
    {
        if (instance == null) instance = this;
        tilePool = new ObjectPool<GameObject>(
            createFunc: CreateTile,
            actionOnGet: OnGetTile,
            actionOnRelease: OnReleaseTile,
            actionOnDestroy: OnDestroyTile,
            false,
            initialPoolSize,
            maxPoolSize
            );
    }

    private GameObject CreateTile()
    {
        GameObject tilePrefab = Instantiate(tile.tilePrefab, transform.position, Quaternion.identity);
        return tilePrefab;
    }

    private void OnGetTile(GameObject tile)
    {
        activeTile.Add(tile);
        tile.SetActive(true);
    }

    private void OnReleaseTile(GameObject tile)
    {
        tile.transform.position = transform.position;
        tile.SetActive(false);
    }

    private void OnDestroyTile(GameObject tile)
    {
        Destroy(tile);
    }

    public GameObject GetTile()
    {
        return tilePool.Get();
    }

    public void ReturnTile(GameObject tile)
    {
        if (activeTile.Contains(tile))
        {
            activeTile.Remove(tile);
            tilePool.Release(tile);
        }
    }
}
