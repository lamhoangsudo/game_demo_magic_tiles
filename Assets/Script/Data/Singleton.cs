using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton Instance { get; private set; }
    public static DataConverter InstanceDataConverter;
    public static GameScoreManager InstanceGameScoreManager;
    public static LevelManager InstanceLevelManager;
    public static PlayerInput InstancePlayerInput;
    public static SpawnManager InstanceSpawnManager;
    public static TilePoolManager InstanceTilePoolManager;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        InstanceDataConverter = (DataConverter)GameObject.FindFirstObjectByType(typeof(DataConverter));
        InstanceGameScoreManager = (GameScoreManager)GameObject.FindFirstObjectByType(typeof(GameScoreManager));
        InstanceLevelManager = (LevelManager)GameObject.FindFirstObjectByType(typeof(LevelManager));
        InstancePlayerInput = (PlayerInput)GameObject.FindFirstObjectByType(typeof(PlayerInput));
        InstanceSpawnManager = (SpawnManager)GameObject.FindFirstObjectByType(typeof(SpawnManager));
        InstanceTilePoolManager = (TilePoolManager)GameObject.FindFirstObjectByType(typeof(TilePoolManager));
    }
}
