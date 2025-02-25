using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScoreManager : MonoBehaviour
{
    public static GameScoreManager instance;
    private int currentPoint = 0;
    private int sumPointNormal = 0;
    private int sumPointGood = 0;
    private int sumPointPerfect = 0;
    private int star = 0;
    [SerializeField] private ScoringLine scoringLine;
    public List<Collider2D> colliderInside { get; private set; } = new();
    public bool CanScore { get; private set; } = false;
    private void Awake()
    {
        if (instance == null) instance = this;
    }
    private void Start()
    {
        scoringLine.OnColliderInsideChange += ScoringLine_OnColliderInsideChange;
        PlayerInput.instance.OnTileTouched += PlayerInput_OnTileTouched;
    }
    private void PlayerInput_OnTileTouched(object sender, List<TileData> tileTouchData)
    {
        foreach (var tile in tileTouchData)
        {
            if (colliderInside.Contains(tile.GetComponent<Collider2D>()))
            {
                currentPoint += EvaluateHit(tile);
                Debug.Log(currentPoint);
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
            case < 0.8f:
                //perfect
                pontAdd = (int)Enum.PointTouchType.Perfect;
                break;
            case < 0.9f:
                //good
                pontAdd = (int)Enum.PointTouchType.Good;
                break;
            case < 10f:
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
        sumPointNormal = (int)Enum.PointTouchType.Normal * TxtAudacityToJsonConverter.Instance.songData.Count;
        sumPointGood = (int)Enum.PointTouchType.Good * TxtAudacityToJsonConverter.Instance.songData.Count;
        sumPointPerfect = (int)Enum.PointTouchType.Perfect * TxtAudacityToJsonConverter.Instance.songData.Count;
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
}
