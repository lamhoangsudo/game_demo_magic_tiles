using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class StarUI : MonoBehaviour
{
    private const string STAR_ANIMATION = "Gain"; // Name of the star animation to play
    [SerializeField] private GameObject[] starIcons; // Array of star icon GameObjects
    [SerializeField] private Animator[] starIconsAnimations; // Array of star icon GameObjects
    private void Start()
    {
        for(int i = 0; i < starIcons.Length; i++)
        {
            starIcons[i].SetActive(false);
            starIconsAnimations[i] = starIcons[i].GetComponent<Animator>();
        }
        Singleton.InstanceGameScoreManager.OnScoreChange += InstanceGameScoreManager_OnScoreChange;
    }

    private void InstanceGameScoreManager_OnScoreChange(object sender, GameScoreManager.OnScoreChangeEventArgs currentPointNormalized)
    {
        UpdateStarUI(currentPointNormalized.CurrentStar);
    }
    private void UpdateStarUI(int star)
    {
        switch (star)
        {
            case 1:
                if (!starIcons[0].activeSelf == false) return;
                starIcons[0].SetActive(true);
                starIconsAnimations[0].Play(STAR_ANIMATION);
                break;
            case 2:
                if (!starIcons[1].activeSelf == false) return;
                starIcons[1].SetActive(true);
                starIconsAnimations[1].Play(STAR_ANIMATION);
                break;
            case 3:
                if (!starIcons[2].activeSelf == false) return;
                starIcons[2].SetActive(true);
                starIconsAnimations[2].Play(STAR_ANIMATION);
                break;
            default:
                return;
        }
    }
}
