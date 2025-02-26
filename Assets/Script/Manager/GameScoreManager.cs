using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScoreManager : MonoBehaviour
{
    public static GameScoreManager instance;
    public int currentPoint { get; private set; } = 0;
    private int sumPointNormal = 0;
    private int sumPointGood = 0;
    public int sumPointPerfect { get; private set; } = 0;
    public int star { get; private set; } = 0;
    [SerializeField] private ScoringLine scoringLine;
    public List<Collider2D> colliderInside { get; private set; } = new();
    public bool CanScore { get; private set; } = false;
    public float currentPointNormalized { get; private set; } = 0;
    private bool isLevelPlay = false;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        scoringLine.OnColliderInsideChange += ScoringLine_OnColliderInsideChange;
        PlayerInput.instance.OnTileTouched += PlayerInput_OnTileTouched;
        LevelManager.instance.OnLevelFinished += LevelManager_OnLevelFinished;
        LevelManager.instance.OnLevelStartPlaying += LevelManager_OnLevelStartPlaying;
        LevelManager.instance.OnLevelPauseAndUnPause += LevelManager_OnLevelPauseAndUnPause;
        CalculateTotalPoints();
    }

    private void LevelManager_OnLevelPauseAndUnPause(object sender, bool isLevelPlay)
    {
        this.isLevelPlay = isLevelPlay;
    }

    private void Update()
    {
        if (isLevelPlay)
        {
            CalculateCurrentPointNormalized();
        }
    }

    private void LevelManager_OnLevelStartPlaying(object sender, EventArgs e)
    {
        isLevelPlay = true;
    }

    private void LevelManager_OnLevelFinished(object sender, EventArgs e)
    {
        StarRating();
        isLevelPlay = false;
    }

    private void PlayerInput_OnTileTouched(object sender, List<TileData> tileTouchData)
    {
        foreach (var tile in tileTouchData)
        {
            if (colliderInside.Contains(tile.GetComponent<Collider2D>()))
            {
                currentPoint += EvaluateHit(tile);
                TilePoolManager.instance.ReturnTile(tile.gameObject);
            }
        }
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
                break;
            case < 0.3f:
                //good
                pontAdd = (int)Enum.PointTouchType.Good;
                break;
            case < 0.5f:
                //nomal
                pontAdd = (int)Enum.PointTouchType.Normal;
                break;
            case >= 1f:
                //miss
                break;
        }
        return pontAdd;
    }

    private void CalculateTotalPoints()
    {
        sumPointNormal = (int)Enum.PointTouchType.Normal * TxtAudacityToDataConverter.Instance.songData.Count;
        sumPointGood = (int)Enum.PointTouchType.Good * TxtAudacityToDataConverter.Instance.songData.Count;
        sumPointPerfect = (int)Enum.PointTouchType.Perfect * TxtAudacityToDataConverter.Instance.songData.Count;
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
