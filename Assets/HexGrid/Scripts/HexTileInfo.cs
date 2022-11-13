using System.Collections.Generic;

public class HexTileInfo
{
    public int index;
    public Dictionary<HexDirection, HexTileInfo> neighbours
        = new Dictionary<HexDirection, HexTileInfo>();
}
