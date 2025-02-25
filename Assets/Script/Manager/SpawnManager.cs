using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;
    [SerializeField] private List<Transform> spawnPosition;
    [SerializeField] private TileDataSO tile;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private List<BeatTileJsonData> songData;
    [SerializeField] private int currentTileIndex;
    private void Awake()
    {
        if (instance == null) instance = this;
        songData = TxtAudacityToJsonConverter.Instance.songData;
        audioSource.Play();
    }
    private void Update()
    {
        if(currentTileIndex < songData.Count && audioSource.time >= songData[currentTileIndex].startTime)
        {
            SpawnTile();
            currentTileIndex++;
        }
    }
    private void SpawnTile()
    {
        Instantiate(tile.tilePrefab, spawnPosition[Random.Range(0, spawnPosition.Count)].position, Quaternion.identity);
    }
}
