using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPositions;
    [SerializeField] private TileDataSO tile;
    private List<BeatTileData> songData;
    public int currentTileIndex;
    [SerializeField] private bool isRamdomSpawned = false;
    public bool isLevelPlaying = false;
    private Vector3 spawnPosition = Vector3.zero;

    private void Start()
    {
        Singleton.InstanceLevelManager.OnLevelStartPlaying += LevelManager_OnLevelStartPlaying;
        Singleton.InstanceLevelManager.OnLevelPauseAndUnPause += LevelManager_OnLevelPauseAndUnPause;
        songData = Singleton.InstanceDataConverter.songData;
    }

    private void LevelManager_OnLevelPauseAndUnPause(object sender, bool isPause)
    {
        this.isLevelPlaying = !isPause;
    }

    private void LevelManager_OnLevelStartPlaying(object sender, System.EventArgs e)
    {
        isLevelPlaying = true;
    }

    private void Update()
    {
        // Check if the current tile index is within the bounds of the song data list and if the level is playing
        if (currentTileIndex < songData.Count && Singleton.InstanceAudioManager.GetAudioTime() >= songData[currentTileIndex].time && isLevelPlaying)
        {
            SpawnTile(currentTileIndex);
            currentTileIndex++;
        }
    }

    private void SpawnTile(int currentTileIndex)
    {
        int lane = songData[currentTileIndex].lane;
        if (isRamdomSpawned)
        {
            // Randomly select a lane, ensuring it is different from the last lane used
            int lastLane = -1;
            int newLane;
            do
            {
                newLane = Random.Range(0, 4);
            } while (newLane == lastLane);
            lastLane = newLane;
            lane = lastLane;
        }
        spawnPosition = spawnPositions[lane].position;
        GameObject tile = Singleton.InstanceTilePoolManager.GetTile();
        tile.transform.position = spawnPosition;
    }
}
