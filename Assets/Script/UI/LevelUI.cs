using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    [SerializeField] private Image bar;
    [SerializeField] private Button ButtonPause;
    private bool isLevelPlaying = false;
    private void Start()
    {
        LevelManager.instance.OnLevelStartPlaying += LevelManager_OnLevelStartPlaying;
        LevelManager.instance.OnLevelFinished += LevelManager_OnLevelFinished;
        ButtonPause.onClick.AddListener(() =>
        {
            LevelManager.instance.OnPause();
        });
    }

    private void LevelManager_OnLevelFinished(object sender, EventArgs e)
    {
        isLevelPlaying = false;
    }

    private void LevelManager_OnLevelStartPlaying(object sender, EventArgs e)
    {
        isLevelPlaying = true;
    }

    private void Update()
    {
        if (isLevelPlaying)
        {
            UpdateBar();
        }
    }
    private void UpdateBar()
    {
        bar.fillAmount = GameScoreManager.instance.currentPointNormalized/3;
    }
}
