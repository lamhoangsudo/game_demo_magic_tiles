using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUI : MonoBehaviour
{
    private void Start()
    {
        PlayerInput.instance.OnStartCountDown += PlayerInput_OnStartCountDown;
        ShowAndHide(true);
    }

    private void PlayerInput_OnStartCountDown(object sender, EventArgs e)
    {
        ShowAndHide(false);
    }

    private void ShowAndHide(bool check)
    {
        gameObject.SetActive(check);
    }
}
