using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HexGridBuilder
{
    // https://github.com/Amaranthos/UnityHexGrid/blob/master/HexGrid/Assets/Grid.cs
    public List<HexTileInfo> HexTileInfos { get; private set; }
        = new List<HexTileInfo>();

    public HexGridBuilder(int count)
    {
        GenerateHexGrid(count);
    }

    private void GenerateHexGrid(int count)
    {
        HexTileInfos.Clear();

        var hexDirections = EnumUtils.GetValues<HexDirection>().ToList();
        int currentHexDirectionIndex = 0;
        
    }

    private HexDirection GetOppositeDirection(HexDirection direction)
    {
        switch (direction)
        {
            case HexDirection.TOP:
                return HexDirection.BOTTOM;
            case HexDirection.TOP_RIGHT:
                return HexDirection.BOTTOM_LEFT;
            case HexDirection.BOTTOM_RIGHT:
                return HexDirection.TOP_LEFT;
            case HexDirection.BOTTOM:
                return HexDirection.TOP;
            case HexDirection.BOTTOM_LEFT:
                return HexDirection.TOP_RIGHT;
            default:
                return HexDirection.BOTTOM_RIGHT;
        }
    }
}
