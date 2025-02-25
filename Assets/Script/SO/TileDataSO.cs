using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Tile", menuName = "ScriptableObjects/TileDataSO")]
public class TileDataSO : ScriptableObject
{
    public int point;
    public GameObject tilePrefab;
}
