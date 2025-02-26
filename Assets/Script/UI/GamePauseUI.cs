using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button homeButton;
    private void Start()
    {
        resumeButton.onClick.AddListener(() =>
        {
            LevelManager.instance.OnPause();
            ShowAndHide(false);
        });
        restartButton.onClick.AddListener(() =>
        {
            LoadScene.Load(Enum.Scene.GamePlayScene);
        });
        homeButton.onClick.AddListener(() =>
        {
            LoadScene.Load(Enum.Scene.MainMenuScene);
        });
        ShowAndHide(false);
        LevelManager.instance.OnLevelPauseAndUnPause += LevelManager_OnLevelPauseAndUnPause;
    }

    private void LevelManager_OnLevelPauseAndUnPause(object sender, bool check)
    {
        ShowAndHide(check);
    }

    private void ShowAndHide(bool check)
    {
        gameObject.SetActive(check);
    }
}
