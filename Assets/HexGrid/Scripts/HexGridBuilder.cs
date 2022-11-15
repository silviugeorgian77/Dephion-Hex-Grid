using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Builds a hex grid starting from the center, then going in spiral clockwise.
/// The spiral starts with the most top center point of the current ring.
///
/// To achieve this, after placing the first tile, we place one above it,
/// then we place (ringIndex * tiles) per each hex edge, then we keep turning
/// right until the current ring is completed.
/// The last side has (ringIndex * tiles - 1) because we already placed the
/// first tile, which is the same as the last tile.
/// After a ring finishes, we move the current position to the right once,
/// so that we can reiterate the steps above.
/// 
/// References:
/// https://stackoverflow.com/questions/2142431/algorithm-for-creating-cells-by-spiral-on-the-hexagonal-field
/// https://github.com/Amaranthos/UnityHexGrid/blob/master/HexGrid/Assets/Grid.cs
/// </summary>
public class HexGridBuilder
{
    public List<HexTileInfo> HexTileInfos { get; private set; }
        = new List<HexTileInfo>();


    private int count;
    private float hexRadius;
    private float padding;
    private int index1d = -1;

    public HexGridBuilder(int count, float hexRadius, float padding)
    {
        this.count = count;
        this.hexRadius = hexRadius;
        this.padding = padding;
        GenerateHexGrid(count, hexRadius, padding);
    }

    private void GenerateHexGrid(
        int count,
        float hexRadius,
        float padding)
    {
        HexTileInfos.Clear();

        if (count <= 0)
        {
            return;
        }

        var x = 0;
        var y = 0;

        AddNewHexTileInfo(x, y);

        if (count == 1)
        {
            return;
        }

        var ringIndex = 1;

        while (index1d < count - 1)
        {
            AddNewHexTileInfo(x, ++y);
            for (int k = 0; k < ringIndex; k++)
            {
                if (HasAddedAllHexTileInfos())
                {
                    break;
                }
                // Move down right
                AddNewHexTileInfo(++x, --y);
            }
            for (int k = 0; k < ringIndex; k++)
            {
                if (HasAddedAllHexTileInfos())
                {
                    break;
                }
                // Move down
                AddNewHexTileInfo(x, --y);
            }
            for (int k = 0; k < ringIndex; k++)
            {
                if (HasAddedAllHexTileInfos())
                {
                    break;
                }
                // Move left
                AddNewHexTileInfo(--x, y);
            }
            for (int k = 0; k < ringIndex; k++)
            {
                if (HasAddedAllHexTileInfos())
                {
                    break;
                }
                // Move up left
                AddNewHexTileInfo(--x, ++y);
            }
            for (int k = 0; k < ringIndex; k++)
            {
                if (HasAddedAllHexTileInfos())
                {
                    break;
                }
                // Move up
                AddNewHexTileInfo(x, ++y);
            }
            for (int k = 0; k < ringIndex - 1; k++)
            {
                if (HasAddedAllHexTileInfos())
                {
                    break;
                }
                // Move up right
                AddNewHexTileInfo(++x, y);
            }
            x++;
            ringIndex++;
        }
    }

    private HexTileInfo CreateHexTileInfo(int x, int y, int index1d)
    {
        var hexTileInfo = new HexTileInfo();
        hexTileInfo.index1d = index1d;
        hexTileInfo.index2d = new Vector2(x, y);
        hexTileInfo.position = new Vector3(
            (hexRadius + padding) * 3f / 2f * x,
            0,
            (hexRadius + padding) * Mathf.Sqrt(3f) * (y + x / 2f)
        );
        return hexTileInfo;
    }

    private void AddNewHexTileInfo(int x, int y)
    {
        index1d++;
        var hexTileInfo = CreateHexTileInfo(x, y, index1d);
        HexTileInfos.Add(hexTileInfo);
    }

    private bool HasAddedAllHexTileInfos()
    {
        return index1d >= count - 1;
    }
}
