using System;
using System.Collections.Generic;
using Newtonsoft.Json;

[Serializable]
public class HexGrid
{
    [JsonProperty("TileSize")]
    public float tileSize;

    [JsonProperty("TilePadding")]
    public float tilePadding;

    [JsonProperty("DefaultTileColor")]
    public string defaultTileColor;

    [JsonProperty("Tiles")]
    public List<HexTile> tiles;
}
