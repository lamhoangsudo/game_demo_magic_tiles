using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    [SerializeField] private Image bar;
    [SerializeField] private Button buttonPause;
    [SerializeField] private TextMeshProUGUI textComboCount;
    private void Start()
    {
        buttonPause.onClick.AddListener(() =>
        {
            Singleton.InstanceLevelManager.OnPause();
        });
        Singleton.InstanceGameScoreManager.OnScoreChange += GameScoreManager_OnScoreChange;
        Singleton.InstanceComboManager.OnComboCountChanged += ComboManager_OnComboCountChanged;
        textComboCount.gameObject.SetActive(false);
    }

    private void ComboManager_OnComboCountChanged(object sender, ComboManager.OnComboCountChangedEventArgs combo)
    {
        UpdateTextCombo(combo.ComboCount, combo.HitType);
    }

    private void GameScoreManager_OnScoreChange(object sender, float currentPointNormalized)
    {
        UpdateBar(currentPointNormalized);
    }

    private void UpdateBar(float currentPointNormalized)
    {
        bar.fillAmount = currentPointNormalized/3;
    }
    private void UpdateTextCombo(int combo, Enum.PointTouchType hitType)
    {
        if(hitType == Enum.PointTouchType.Perfect)
        {
            textComboCount.text = $"Perfect x {combo}";
            if(textComboCount.gameObject.activeSelf == false)
            {
                textComboCount.gameObject.SetActive(true);
            }
        }
        else if(hitType == Enum.PointTouchType.Good)
        {
            textComboCount.text = $"Good x {combo}";
            if (textComboCount.gameObject.activeSelf == false)
            {
                textComboCount.gameObject.SetActive(true);
            }
        }
        else if(hitType == Enum.PointTouchType.Normal)
        {
            textComboCount.gameObject.SetActive(false);
        }
    }
}
