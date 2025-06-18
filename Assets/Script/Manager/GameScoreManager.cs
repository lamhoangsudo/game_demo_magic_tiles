using System;
using System.Collections;
using System.Collections.Generic;
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
    public event EventHandler<float> OnScoreChange;
    public event EventHandler<Enum.PointTouchType> OnTypeHit;

    private void Start()
    {
        scoringLine.OnColliderInsideChange += ScoringLine_OnColliderInsideChange;
        Singleton.InstancePlayerInput.OnTileTouched += PlayerInput_OnTileTouched;
        Singleton.InstanceLevelManager.OnLevelFinished += LevelManager_OnLevelFinished;
        CalculateTotalPoints();
    }

    private void LevelManager_OnLevelFinished(object sender, EventArgs e)
    {
        StarRating();
    }

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
        OnScoreChange?.Invoke(this, currentPointNormalized);
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
                OnTypeHit?.Invoke(this, Enum.PointTouchType.Perfect);
                break;
            case < 0.3f:
                //good
                pontAdd = (int)Enum.PointTouchType.Good;
                OnTypeHit?.Invoke(this, Enum.PointTouchType.Good);
                break;
            case >= 0.3f:
                //nomal
                pontAdd = (int)Enum.PointTouchType.Normal;
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
        if(currentPoint <= sumPointPerfect && currentPoint > sumPointGood)
        {
            star = (int)Enum.StarRatingType.Perfect;
        }
        else if(currentPoint <= sumPointGood && currentPoint > sumPointNormal)
        {
            star = (int)Enum.StarRatingType.Good;
        }
        else if (currentPoint <= sumPointNormal && currentPoint > 0)
        {
            star = (int)Enum.StarRatingType.Normal;
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
}
