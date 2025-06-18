using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    private bool isLevelPlaying = false;
    private void Start()
    {
        Singleton.InstanceLevelManager.OnLevelPauseAndUnPause += LevelManager_OnLevelPauseAndUnPause;
        Singleton.InstanceLevelManager.OnLevelStartPlaying += LevelManager_OnLevelStartPlaying;
        Singleton.InstanceLevelManager.OnLevelFinished += LevelManager_OnLevelFinished;
    }

    private void LevelManager_OnLevelFinished(object sender, EventArgs e)
    {
        Stop();
    }

    private void LevelManager_OnLevelPauseAndUnPause(object sender, bool isLevelPlaying)
    {
        this.isLevelPlaying = isLevelPlaying;
        if (isLevelPlaying)
        {
            Pause();
        }
        else
        {
            UnPause();
        }
    }
    private void LevelManager_OnLevelStartPlaying(object sender, System.EventArgs e)
    {
        audioSource.Play();
        isLevelPlaying = true;
    }
    public void Play()
    {
        audioSource.Play();
    }
    public void Pause()
    {
        audioSource.Pause();
    }
    public void UnPause()
    {
        audioSource.UnPause();
    }
    public float GetAudioTime()
    {
        return audioSource.time;
    }
    public void Stop()
    {
        audioSource.Stop();
    }
}
