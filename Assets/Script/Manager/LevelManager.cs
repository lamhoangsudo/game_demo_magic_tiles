using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private bool IsMusicFinished;
    private bool IsAllNotesGone;
    public int totalNode;
    private bool isPause;
    [SerializeField] private float CountDownTimeMax;
    public float time { get; private set; }
    public Enum.LevelState levelState { get; private set; }
    [SerializeField] private DestroyLine destroyLine;
    [SerializeField] private ScoringLine scoringLine;
    [SerializeField] private AudioSource audioSource;
    private List<Collider2D> colliderInside;

    public event EventHandler OnLevelFinished;
    public event EventHandler<bool> OnLevelPauseAndUnPause;
    public event EventHandler<bool> OnLevelCountDown;
    public event EventHandler OnLevelStartPlaying;

    private void Start()
    {
        levelState = Enum.LevelState.Start;
        totalNode = Singleton.InstanceDataConverter.songData.Count;
        destroyLine.OnTileTougchDestroyLine += DestroyLine_OnTileTougchDestroyLine;
        scoringLine.OnColliderInsideChange += ScoringLine_OnColliderInsideChange;
        Singleton.InstancePlayerInput.OnStartCountDown += PlayerInput_OnStartCountDown;
        Singleton.InstancePlayerInput.OnTileTouched += PlayerInput_OnTileTouched;
        time = CountDownTimeMax;
    }

    private void ScoringLine_OnColliderInsideChange(object sender, List<Collider2D> colliderInside)
    {
        this.colliderInside = colliderInside;
    }

    private void PlayerInput_OnTileTouched(object sender, List<TileData> tileTouchData)
    {
        foreach (var tile in tileTouchData)
        {
            if (colliderInside.Contains(tile.GetComponent<Collider2D>()))
            {
                totalNode--;
                IsAllNotesGone = totalNode == 0;
                Singleton.InstanceTilePoolManager.ReturnTile(tile.gameObject);
            }
        }
    }

    private void PlayerInput_OnStartCountDown(object sender, EventArgs e)
    {
        levelState = Enum.LevelState.CountDown;
        OnLevelCountDown?.Invoke(this, true);
    }

    private void DestroyLine_OnTileTougchDestroyLine(object sender, EventArgs e)
    {
        levelState = Enum.LevelState.Finished;
    }

    private void Update()
    {
        ChangeLevelState();
    }

    private void ChangeLevelState()
    {
        switch (levelState)
        {
            case Enum.LevelState.Start:
                Time.timeScale = 0;
                break;
            case Enum.LevelState.CountDown:
                Time.timeScale = 1;
                time -= Time.deltaTime;
                if (time <= 0)
                {
                    OnLevelCountDown?.Invoke(this, false);
                    OnLevelStartPlaying?.Invoke(this, EventArgs.Empty);
                    time = CountDownTimeMax;
                    levelState = Enum.LevelState.Playing;
                }
                break;
            case Enum.LevelState.Playing:
                Time.timeScale = 1;
                IsMusicFinished = audioSource.time >= audioSource.clip.length;
                if (IsMusicFinished && IsAllNotesGone)
                {
                    levelState = Enum.LevelState.Finished;
                }
                break;
            case Enum.LevelState.Finished:
                OnLevelFinished?.Invoke(this, EventArgs.Empty);
                Time.timeScale = 0;
                break;
            case Enum.LevelState.Pause:
                break;
        }
    }

    public string GetStringPoint()
    {
        return $"Score: {Singleton.InstanceGameScoreManager.currentPoint}/{Singleton.InstanceGameScoreManager.sumPointPerfect}";
    }

    public void OnPause()
    {
        if (levelState == Enum.LevelState.Playing || levelState == Enum.LevelState.Pause)
        {
            levelState = Enum.LevelState.Pause;
            if (!isPause)
            {
                isPause = true;
                Time.timeScale = 0;
                levelState = Enum.LevelState.Pause;
                OnLevelPauseAndUnPause?.Invoke(this, isPause);
            }
            else
            {
                isPause = false;
                Time.timeScale = 1;
                levelState = Enum.LevelState.Playing;
                OnLevelPauseAndUnPause?.Invoke(this, isPause);
            }
        }
    }
}
