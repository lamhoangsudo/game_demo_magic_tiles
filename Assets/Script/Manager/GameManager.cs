using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private bool IsMusicFinished;
    private bool IsAllNotesGone;
    private int totalNode;
    [SerializeField] private DestroyLine destroyLine;
    [SerializeField] private AudioSource audioSource;
    private void Awake()
    {
        if (instance == null) instance = this;
    }
    private void Start()
    {
        totalNode = TxtAudacityToJsonConverter.Instance.songData.Count;
        destroyLine.OnTileTougchDestroyLine += DestroyLine_OnTileTougchDestroyLine;
    }

    private void DestroyLine_OnTileTougchDestroyLine(object sender, EventArgs e)
    {
        totalNode--;
        IsAllNotesGone = totalNode == 0;
    }
    private void Update()
    {
        IsMusicFinished = audioSource.time >= audioSource.clip.length;
        if(IsMusicFinished)
        {
            if (IsAllNotesGone)
            {
                Debug.Log("You Win");
            }
            else
            {
                Debug.Log("You Lose");
            }
        }
    }
}
