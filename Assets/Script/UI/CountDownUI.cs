using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountDownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countDownText;
    [SerializeField] private AudioClip audioClip;
    private Animator animator;
    private int lastNumber;
    private const string COUNTDOWN_ANIMATOR_TRIGGER = "CountDown";
    private void Start()
    {
        animator = countDownText.gameObject.GetComponent<Animator>();
        Singleton.InstanceLevelManager.OnLevelCountDown += LevelManager_OnLevelCountDown;
        ShowAndHide(false);
    }

    private void LevelManager_OnLevelCountDown(object sender, bool check)
    {
        ShowAndHide(check);
    }

    private void Update()
    {
        int numberCountDown = Mathf.CeilToInt(Singleton.InstanceLevelManager.time);
        if (lastNumber != numberCountDown)
        {
            lastNumber = numberCountDown;
            animator.SetTrigger(COUNTDOWN_ANIMATOR_TRIGGER);
            countDownText.text = numberCountDown.ToString();
            AudioSource.PlayClipAtPoint(audioClip, Vector2.zero);
        }
    }
    private void ShowAndHide(bool check)
    {
        gameObject.SetActive(check);
    }
}
