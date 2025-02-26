using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Enum
{
    public enum WindowSizeFMOD
    {
        VeryLow = 256,
        Low = 512,
        High = 1024,
        VeryHigh = 2048,
    }
    public enum PointTouchType
    {
        Perfect = 3,
        Good = 2,
        Normal = 1,
        Miss = 0,
    }
    public enum StarRatingType
    {
        None = 0,
        Normal = 1,
        Good = 2,
        Perfect = 3,
    }
    public enum Scene
    {
        MainMenuScene,
        LoadingScene,
        GamePlayScene,
    }
    public enum LevelState
    {
        Start,
        CountDown,
        Playing,
        Finished,
        Pause,
    }
}
