using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New Level Tile", menuName = "Tiles/LevelTile")]
public class LevelTile : Tile
{
    public bool CanPlace = false;
}
