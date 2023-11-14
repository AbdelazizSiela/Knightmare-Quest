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
        // Loop through the entire Tilemap and replace grass pattern 1 with grass pattern 2 randomly.
        BoundsInt bounds = tilemap.cellBounds;

        foreach (Vector3Int position in bounds.allPositionsWithin)
        {
            if (tilemap.GetTile(position) == grassPattern1 && !sideGrass.Contains(tilemap.GetTile(position)))
            {
                if (Random.value < randomTileChance)
                {
                    tilemap.SetTile(position, grassPattern2);
                }
            }
        }
    }
}
