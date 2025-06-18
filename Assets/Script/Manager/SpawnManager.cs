using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;
    [SerializeField] private List<Transform> spawnPosition;
    [SerializeField] private TileDataSO tile;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private List<BeatTileData> songData;
    [SerializeField] private int currentTileIndex;
    private bool isLevelPlaying = false;
    private Vector3 currentSpawnPosition = Vector3.zero;
    private Vector3 lastSpawnPosition = Vector3.zero;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        LevelManager.instance.OnLevelStartPlaying += LevelManager_OnLevelStartPlaying;
        LevelManager.instance.OnLevelPauseAndUnPause += LevelManager_OnLevelPauseAndUnPause;
        songData = DataConverter.Instance.songData;
    }

    private void LevelManager_OnLevelPauseAndUnPause(object sender, bool isLevelPlaying)
    {
        this.isLevelPlaying = isLevelPlaying;
        if(isLevelPlaying)
        {
            audioSource.Pause();
            
        }
        else
        {
            audioSource.UnPause();
        }
    }

    private void LevelManager_OnLevelStartPlaying(object sender, System.EventArgs e)
    {
        audioSource.Play();
        isLevelPlaying = true;
    }

    private void Update()
    {
        if(currentTileIndex < songData.Count && audioSource.time >= songData[currentTileIndex].time && isLevelPlaying)
        {
            SpawnTile(currentTileIndex);
            currentTileIndex++;
        }
    }

    private void SpawnTile(int currentTileIndex)
    {
        do
        {
            currentSpawnPosition = spawnPosition[songData[currentTileIndex].lane].position;
        }
        while(currentSpawnPosition == lastSpawnPosition);
        GameObject tile = TilePoolManager.instance.GetTile();
        tile.transform.position = currentSpawnPosition;
        lastSpawnPosition = currentSpawnPosition;
    }
}
