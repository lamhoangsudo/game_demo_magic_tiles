using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    private bool IsMusicFinished;
    private bool IsAllNotesGone;
    private int totalNode;
    private bool isPause;
    [SerializeField] private float CountDownTimeMax;
    public float time { get; private set; }
    public Enum.LevelState levelState { get; private set; }
    [SerializeField] private DestroyLine destroyLine;
    [SerializeField] private AudioSource audioSource;
    public event EventHandler OnLevelFinished;
    public event EventHandler<bool> OnLevelPauseAndUnPause;
    public event EventHandler<bool> OnLevelCountDown;
    public event EventHandler OnLevelStartPlaying;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        levelState = Enum.LevelState.Start;
        totalNode = DataConverter.Instance.songData.Count;
        destroyLine.OnTileTougchDestroyLine += DestroyLine_OnTileTougchDestroyLine;
        PlayerInput.instance.OnStartCountDown += PlayerInput_OnStartCountDown;
        time = CountDownTimeMax;
    }

    private void PlayerInput_OnStartCountDown(object sender, EventArgs e)
    {
        levelState = Enum.LevelState.CountDown;
        OnLevelCountDown?.Invoke(this, true);
    }

    private void DestroyLine_OnTileTougchDestroyLine(object sender, EventArgs e)
    {
        totalNode--;
        IsAllNotesGone = totalNode == 0;
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
        return $"Score: {GameScoreManager.instance.currentPoint}/{GameScoreManager.instance.sumPointPerfect}";
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
