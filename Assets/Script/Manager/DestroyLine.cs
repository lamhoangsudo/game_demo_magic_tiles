using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyLine : MonoBehaviour
{
    private List<Collider2D> colliderInside = new();
    public Collider2D triggerCollider { get; private set; }
    public event EventHandler OnTileTougchDestroyLine;
    private void Start()
    {
        triggerCollider = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            DestroyListColliderInside();
            OnTileTougchDestroyLine?.Invoke(this, EventArgs.Empty);
        }
    }
    private void DestroyListColliderInside()
    {
        if(colliderInside.Count > 0)
        {
            colliderInside.Clear();
        }
        ContactFilter2D contactFilter2D = new();
        triggerCollider.OverlapCollider(contactFilter2D, colliderInside);
        foreach (Collider2D collider in colliderInside)
        {
            TilePoolManager.instance.ReturnTile(collider.gameObject);
        }
        colliderInside.Clear();
    }
}
