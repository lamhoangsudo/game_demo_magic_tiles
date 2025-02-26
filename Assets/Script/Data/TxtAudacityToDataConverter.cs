using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
public class TxtAudacityToDataConverter : MonoBehaviour
{
    public static TxtAudacityToDataConverter Instance;
    public List<BeatTileData> songData { get; private set; } = new List<BeatTileData>();
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
    private void Start()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "BeatLayer1.txt");

        if (File.Exists(path))
        {
            string[] lines = File.ReadAllLines(path);
            for(int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split('\t');
                if (parts.Length >= 2)
                {
                    float startTime = float.Parse(parts[0]);
                    float endTime = float.Parse(parts[1]);
                    songData.Add(new BeatTileData()
                    {
                        startTime = startTime,
                        endTime = endTime,
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
    }
}
