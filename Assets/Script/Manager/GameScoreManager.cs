using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GameScoreManager : MonoBehaviour
{
    public int currentPoint { get; private set; } = 0;
    private int sumPointNormal = 0;
    private int sumPointGood = 0;
    public int sumPointPerfect { get; private set; } = 0;
    public int star { get; private set; } = 0;
    [SerializeField] private ScoringLine scoringLine;
    public List<Collider2D> colliderInside { get; private set; } = new();
    public bool CanScore { get; private set; } = false;
    private float currentPointNormalized = 0;
    public event EventHandler<OnScoreChangeEventArgs> OnScoreChange;
    public event EventHandler<Enum.PointTouchType> OnTypeHit;
    public class OnScoreChangeEventArgs : EventArgs
    {
        public int CurrentStar { get; set; }
        public float CurrentPointNormalized { get; set; }
    }

    private void Start()
    {
        scoringLine.OnColliderInsideChange += ScoringLine_OnColliderInsideChange;
        Singleton.InstancePlayerInput.OnTileTouched += PlayerInput_OnTileTouched;
        //Singleton.InstanceLevelManager.OnDebug += InstanceLevelManager_OnDebug;
        CalculateTotalPoints();
    }

    //private void InstanceLevelManager_OnDebug(object sender, EventArgs e)
    //{
    //    currentPoint+= (int)Enum.PointTouchType.Perfect;
    //    CalculateCurrentPointNormalized();
    //    OnScoreChange?.Invoke(this, currentPointNormalized);
    //}

    private void PlayerInput_OnTileTouched(object sender, List<TileData> tileTouchData)
    {
        foreach (var tile in tileTouchData)
        {
            if (colliderInside.Contains(tile.GetComponent<Collider2D>()))
            {
                currentPoint += EvaluateHit(tile);
            }
        }
        CalculateCurrentPointNormalized();
        OnScoreChange?.Invoke(this, new OnScoreChangeEventArgs
        {
            CurrentPointNormalized = currentPointNormalized,
            CurrentStar = star
        });
    }

    private void ScoringLine_OnColliderInsideChange(object sender, List<Collider2D> colliderInside)
    {
        this.colliderInside = colliderInside;
        if (colliderInside.Count > 0)
        {
            CanScore = true;
        }
        else
        {
            CanScore = false;
        }
    }

    private int EvaluateHit(TileData tileData)
    {
        float timeDifference = tileData.playerTouchTime - tileData.scoringLineTouchTime;
        int pontAdd = 1;
        switch (timeDifference)
        {
            case < 0.1f:
                //perfect
                pontAdd = (int)Enum.PointTouchType.Perfect;
                StarRating();
                OnTypeHit?.Invoke(this, Enum.PointTouchType.Perfect);
                break;
            case < 0.3f:
                //good
                pontAdd = (int)Enum.PointTouchType.Good;
                StarRating();
                OnTypeHit?.Invoke(this, Enum.PointTouchType.Good);
                break;
            case >= 0.3f:
                //nomal
                pontAdd = (int)Enum.PointTouchType.Normal;
                StarRating();
                OnTypeHit?.Invoke(this, Enum.PointTouchType.Normal);
                break;
        }
        return pontAdd;
    }

    private void CalculateTotalPoints()
    {
        sumPointNormal = (int)Enum.PointTouchType.Normal * Singleton.InstanceDataConverter.songData.Count;
        sumPointGood = (int)Enum.PointTouchType.Good * Singleton.InstanceDataConverter.songData.Count;
        sumPointPerfect = (int)Enum.PointTouchType.Perfect * Singleton.InstanceDataConverter.songData.Count;
    }

    private void StarRating()
    {
        float pointNormalized = currentPointNormalized / 3;
        pointNormalized = math.clamp(pointNormalized, 0f, 1f); // Ensure the value is between 0 and 1
        if (pointNormalized >= 1f / 3f && pointNormalized < 2f / 3f)
        {
            star = 1;
        }
        else if (pointNormalized >= 2f / 3f && pointNormalized < 1f)
        {
            star = 2;
        }
        else if (pointNormalized >= 1f)
        {
            star = 3;
        }
        else
        {
            return;
        }
    }

    private void CalculateCurrentPointNormalized()
    {
        if (currentPoint <= sumPointPerfect && currentPoint > sumPointGood)
        {
            currentPointNormalized = (float)currentPoint / sumPointPerfect * 3;
        }
        else if (currentPoint <= sumPointGood && currentPoint > sumPointNormal)
        {
            currentPointNormalized = (float)currentPoint / sumPointGood * 2;
        }
        else if (currentPoint <= sumPointNormal && currentPoint > 0)
        {
            currentPointNormalized = (float)currentPoint / sumPointNormal * 1;
        }
    }
    public string GetStringPoint()
    {
        return $"Score: {currentPoint}/{sumPointPerfect}";
    }
    public string GetStringStar()
    {
        return $"Star: {Singleton.InstanceGameScoreManager.star}";
    }
}
