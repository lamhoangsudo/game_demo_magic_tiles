using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    [SerializeField] private Image bar;
    [SerializeField] private Button ButtonPause;
    private void Start()
    {
        ButtonPause.onClick.AddListener(() =>
        {
            Singleton.InstanceLevelManager.OnPause();
        });
        Singleton.InstanceGameScoreManager.OnScoreChange += GameScoreManager_OnScoreChange;
    }

    private void GameScoreManager_OnScoreChange(object sender, float currentPointNormalized)
    {
        UpdateBar(currentPointNormalized);
    }

    private void UpdateBar(float currentPointNormalized)
    {
        bar.fillAmount = currentPointNormalized/3;
    }
}
