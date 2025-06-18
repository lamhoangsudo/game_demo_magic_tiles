using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    private bool isHit = false;
    public event EventHandler<List<TileData>> OnTileTouched;
    public event EventHandler OnStartCountDown;
    [SerializeField] private GameObject hitEffect;

    private void Update()
    {
        if(isHit && !Singleton.InstanceGameScoreManager.CanScore) return;
        else
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    if (Singleton.InstanceLevelManager.levelState == Enum.LevelState.Start)
                    {
                        OnStartCountDown?.Invoke(this, EventArgs.Empty);
                        return;
                    }
                    if (Singleton.InstanceLevelManager.levelState != Enum.LevelState.Playing) return;
                    Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                    List<RaycastHit2D> raycastHit2DS = Physics2D.RaycastAll(touchPosition, Vector2.zero, 999f, layerMask).ToList();
                    if (raycastHit2DS.Count <= 0 || raycastHit2DS.IsUnityNull()) return;
                    TileTapped(raycastHit2DS);
                    isHit = false;
                }
            }
        }
    }

    private void TileTapped(List<RaycastHit2D> raycastHit2DS)
    {
        List<TileData> tileDatas = new();
        foreach (RaycastHit2D hit in raycastHit2DS)
        {
            if (hit.collider == null) continue;
            TileData tileData = hit.collider.GetComponent<TileData>();
            if (!tileData.canGetPoints) continue;
            tileData.playerTouchTime = Time.time;
            tileData.canGetPoints = false;
            tileDatas.Add(tileData);
            Instantiate(hitEffect, new Vector3(tileData.gameObject.transform.position.x, transform.position.y, -1f), Quaternion.identity);
        }
        OnTileTouched?.Invoke(this, tileDatas);
        isHit = true;        
    }
}
