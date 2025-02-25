using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScoringLine : MonoBehaviour
{
    private List<Collider2D> colliderInside = new();
    public Collider2D triggerCollider { get; private set; }
    public event EventHandler<List<Collider2D>> OnColliderInsideChange;
    private void Start()
    {
        triggerCollider = GetComponent<Collider2D>();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            GetListColliderInside();
            SetDataColliderInside();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            GetListColliderInside();
        }
    }
    private void GetListColliderInside()
    {
        colliderInside.Clear();
        ContactFilter2D contactFilter2D = new();
        triggerCollider.OverlapCollider(contactFilter2D, colliderInside);
        OnColliderInsideChange?.Invoke(this, colliderInside);
    }
    private void SetDataColliderInside()
    {
        foreach (var collider in colliderInside)
        {
            collider.GetComponent<TileData>().scoringLineTouchTime = Time.time;
        }
    }
}
