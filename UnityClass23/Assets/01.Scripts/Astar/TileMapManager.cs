using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapManager
{
    private Tilemap _floorMap;
    private Tilemap _collsionMap;

    public static TileMapManager Instance = null;

    public TileMapManager(Transform tileMapParent)
    {
        _floorMap = tileMapParent.Find("Floor").GetComponent<Tilemap>();
        _collsionMap = tileMapParent.Find("Collisions").GetComponent<Tilemap>();

        _floorMap.CompressBounds();
    }

    public bool CanMove(Vector3Int pos)
    {
        BoundsInt mapBound = _floorMap.cellBounds;
        //¸Ê ¹ÛÀ¸·Î ¸ø³ª°¡°Ô
        if (pos.x < mapBound.xMin || pos.x > mapBound.xMax || pos.y < mapBound.yMin || pos.y > mapBound.yMax)
        {
            return false;
        }

        return _collsionMap.GetTile(pos) == null;
    }

    public Vector3Int GetTilePos(Vector3 worldPos)
    {
        return _floorMap.WorldToCell(worldPos); //¿ùµåÁÂÇ¥¸¦ ³ÖÀ¸¸é Å¸ÀÏ¸Ê ¼¿ÁÂÇ¥¸¦ °¡Á®¿È
    }

    public Vector3 GetWorldPos(Vector3Int cellpos)
    {
        return _floorMap.GetCellCenterWorld(cellpos);
    }
}
