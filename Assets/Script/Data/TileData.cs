using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TileData : MonoBehaviour
{
    [SerializeField] private float fallSpeed;
    [SerializeField] private TileDataSO tileDataSO;
    public float scoringLineTouchTime;
    public float playerTouchTime;
    private void Update()
    {
        Fall();
    }
    private void Fall()
    {
        transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);
    }
    public void ResetData()
    {
        scoringLineTouchTime = 0;
        playerTouchTime = 0;
    }
    public void OnDestroy()
    {
        ResetData();
        Destroy(gameObject);
    }
    private void OnDisable()
    {
        ResetData();
    }
}
