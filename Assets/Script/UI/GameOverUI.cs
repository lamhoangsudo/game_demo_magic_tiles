using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Button homeButton;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI starText;
    private void Start()
    {
        restartButton.onClick.AddListener(() =>
        {
            LoadScene.Load(Enum.Scene.GamePlayScene);
        });
        homeButton.onClick.AddListener(() =>
        {
            LoadScene.Load(Enum.Scene.MainMenuScene);
        });
        scoreText.text = LevelManager.instance.GetStringPoint();
        starText.text = $"Star: {GameScoreManager.instance.star}";
        ShowAndHide(false);
        LevelManager.instance.OnLevelFinished += LevelManager_OnLevelFinished;
    }

    private void LevelManager_OnLevelFinished(object sender, System.EventArgs e)
    {
        ShowAndHide(true);
    }

    private void ShowAndHide(bool check)
    {
        gameObject.SetActive(check);
    }
}
