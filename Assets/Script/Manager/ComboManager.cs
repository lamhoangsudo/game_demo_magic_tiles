using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    private int comboCount = 0;
    private Enum.PointTouchType lastHitType = Enum.PointTouchType.Normal;
    private Enum.PointTouchType currentHitType;
    public event EventHandler<OnComboCountChangedEventArgs> OnComboCountChanged;
    public class OnComboCountChangedEventArgs : EventArgs
    {
        public int ComboCount;
        public Enum.PointTouchType HitType;
    }
    private void Start()
    {
        Singleton.InstanceGameScoreManager.OnTypeHit += GameScoreManager_OnTypeHit;
    }

    private void GameScoreManager_OnTypeHit(object sender, Enum.PointTouchType pointTouchType)
    {
        currentHitType = pointTouchType;
        switch (pointTouchType)
        {
            case Enum.PointTouchType.Perfect:
                ComboCheck();
                break;
            case Enum.PointTouchType.Good:
                ComboCheck();
                break;
            case Enum.PointTouchType.Normal:
                comboCount = 0; // Reset combo on normal hit
                lastHitType = currentHitType;
                break;
        }
    }
    private void ComboCheck()
    {
        if (comboCount == 0)
        {
            // Start a new combo if this is the first hit
            lastHitType = currentHitType;
            comboCount++;
            OnComboCountChanged?.Invoke(this, new OnComboCountChangedEventArgs
            {
                ComboCount = comboCount,
                HitType = currentHitType
            });
        }
        else
        {
            // Increment combo if the hit type is the same as the last one
            if (lastHitType == currentHitType)
            {
                comboCount++;
                OnComboCountChanged?.Invoke(this, new OnComboCountChangedEventArgs
                {
                    ComboCount = comboCount,
                    HitType = currentHitType
                });
            }
            else
            {
                // If the hit type is different, reset combo count based on hit type
                if (currentHitType == Enum.PointTouchType.Perfect || currentHitType == Enum.PointTouchType.Good)
                {
                    // Reset combo if the hit type is different
                    comboCount = 1;
                    OnComboCountChanged?.Invoke(this, new OnComboCountChangedEventArgs
                    {
                        ComboCount = comboCount,
                        HitType = currentHitType
                    });
                }
                else
                {
                    // Reset combo on normal hit
                    comboCount = 0;
                }
                lastHitType = currentHitType;
            }
        }
    }
}
