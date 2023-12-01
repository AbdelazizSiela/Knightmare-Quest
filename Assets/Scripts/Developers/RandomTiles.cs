using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RandomTiles : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase grassPattern1;
    public TileBase grassPattern2;

    public TileBase[] sideGrass;

    [Range(0, 1)]
    public float randomTileChance = 0.5f;

    void Start()
    {
        //// Loop through the entire Tilemap and replace grass pattern 1 with grass pattern 2 randomly.
        //BoundsInt bounds = tilemap.cellBounds;

        //foreach (Vector3Int position in bounds.allPositionsWithin)
        //{
        //    if (tilemap.GetTile(position) == grassPattern1 && !sideGrass.Contains(tilemap.GetTile(position)))
        //    {
        //        if (Random.value < randomTileChance)
        //        {
        //            tilemap.SetTile(position, grassPattern2);
        //        }
        //    }
        //}
        CopyTiles();
    }

    public Tilemap grassTilemap;
    public Tilemap dirtTilemap;

    void CopyTiles()
    {
        BoundsInt bounds = grassTilemap.cellBounds;

        for (int x = bounds.x; x < bounds.x + bounds.size.x; x++)
        {
            for (int y = bounds.y; y < bounds.y + bounds.size.y; y++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);
                TileBase dirtTIle = grassTilemap.GetTile(pos);

                if (dirtTIle != null && dirtTIle == grassPattern1)
                {
                    dirtTilemap.SetTile(pos, dirtTIle);
                    grassTilemap.SetTile(pos, null);
                }
            }
        }
    }
}
    


