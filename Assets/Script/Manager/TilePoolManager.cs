using UnityEngine;
using UnityEngine.Pool;

public class TilePoolManager : MonoBehaviour
{
    public static TilePoolManager instance;
    [SerializeField] private TileDataSO tile;
    [SerializeField] private int initialPoolSize = 10;
    [SerializeField] private int maxPoolSize = 20;
    private ObjectPool<GameObject> tilePool;
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
        return Instantiate(tile.tilePrefab);
    }

    private void OnGetTile(GameObject tile)
    {
        tile.SetActive(true);
    }

    private void OnReleaseTile(GameObject tile)
    {
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
        tilePool.Release(tile);
    }
}
