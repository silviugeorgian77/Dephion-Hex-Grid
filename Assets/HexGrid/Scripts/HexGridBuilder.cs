using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// References:
// https://stackoverflow.com/questions/2142431/algorithm-for-creating-cells-by-spiral-on-the-hexagonal-field
// https://github.com/Amaranthos/UnityHexGrid/blob/master/HexGrid/Assets/Grid.cs

public class HexGridBuilder
{
    public List<HexTileInfo> HexTileInfos { get; private set; }
        = new List<HexTileInfo>();


    private int count;
    private float hexRadius;
    private float padding;
    private int index1d;

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

        var hexTileInfo = CreateHexTileInfo(x, y, index1d);
        HexTileInfos.Add(hexTileInfo);

        if (count == 1)
        {
            return;
        }

        var ringIndex = 1;
        while (index1d < count - 1)
        {
            for (int k = 0; k < ringIndex; k++)
            {
                if (HasAddedAllHexTileInfos())
                {
                    break;
                }
                // Move up
                AddNewHexTileInfo(x, ++y);
            }
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
