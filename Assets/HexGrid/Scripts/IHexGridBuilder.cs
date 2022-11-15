using System.Collections.Generic;

public interface IHexGridBuilder
{
    List<HexTileInfo> GetHexTileInfos(
        int count,
        float hexRadius,
        float padding);
}
