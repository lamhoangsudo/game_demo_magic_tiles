﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using System;
public class DataConverter : MonoBehaviour
{
    public event EventHandler OnDataConvertedComplete;
    [SerializeField] private string jsonFileName;
    public List<BeatTileData> songData { get; private set; } = new List<BeatTileData>();
    private void Awake()
    {
        // Read JSON file from StreamingAssets folder
        string path = Path.Combine(Application.streamingAssetsPath, $"{jsonFileName}.json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            songData = JsonConvert.DeserializeObject<List<BeatTileData>>(json);
            OnDataConvertedComplete?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Debug.LogError(" not found file json");
        }
        /*
        string path = Path.Combine(Application.streamingAssetsPath, "BeatLayer1.txt");

        if (File.Exists(path))
        {
            string[] lines = File.ReadAllLines(path);
            for (int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split('\t');
                if (parts.Length >= 2)
                {
                    float startTime = float.Parse(parts[0]);
                    float endTime = float.Parse(parts[1]);
                    songData.Add(new BeatTileData()
                    {
                        time = startTime,
                    });
                }
            }

            //string json = JsonUtility.ToJson(jsonList.songData, true);

            //string jsonPath = Path.Combine(Application.streamingAssetsPath, "songData.json");
            //File.WriteAllText(jsonPath, json);

            //Debug.Log("Chuyển đổi hoàn tất! JSON đã lưu tại: " + jsonPath);
        }
        else
        {
            Debug.LogError("Không tìm thấy file trong StreamingAssets!");
        }
        */
    }
}
