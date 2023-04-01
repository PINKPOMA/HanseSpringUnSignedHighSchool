using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileChange : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase[] newTile;

    void Start()
    {
        // 타일맵의 모든 타일을 변경합니다.
        foreach (Vector3Int position in tilemap.cellBounds.allPositionsWithin)
        {
            // 현재 타일을 가져옵니다.
            TileBase tile = tilemap.GetTile(position);
            // 타일이 존재하고, 원래 있던 타일이라면 변경합니다.
            if (tile != null)
            {
                tilemap.SetTile(position, newTile[Random.Range(0,newTile.Length)]);
            }
        }
    }
}
