using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput instance;
    [SerializeField] private LayerMask layerMask;
    private bool isHit = false;
    public event EventHandler<List<TileData>> OnTileTouched;
    private void Awake()
    {
        if (instance == null) instance = this;
    }
    private void Update()
    {
        if(isHit && !GameScoreManager.instance.CanScore) return;
        foreach(Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                List<RaycastHit2D> raycastHit2DS = Physics2D.RaycastAll(touchPosition, Vector2.zero, 999f, layerMask).ToList();
                if (raycastHit2DS.Count <= 0 || raycastHit2DS.IsUnityNull()) return;
                TileTapped(raycastHit2DS);
                isHit = false;
            }
        }
    }
    private void TileTapped(List<RaycastHit2D> raycastHit2DS)
    {
        List<TileData> tileDatas = new();
        foreach (RaycastHit2D hit in raycastHit2DS)
        {
            if (hit.collider == null) return;
            TileData tileData = hit.collider.GetComponent<TileData>();
            tileData.playerTouchTime = Time.time;
            tileDatas.Add(tileData);
        }
        
        OnTileTouched?.Invoke(this, tileDatas);
        isHit = true;        
        Debug.Log("hit");
    }
}
