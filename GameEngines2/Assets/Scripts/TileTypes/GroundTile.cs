﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Game/Tiles/Ground")]
public class GroundTile : Tile
{
    public bool diggable;
    public GroundTile dugVersion;
    public bool Dug = false;
}
