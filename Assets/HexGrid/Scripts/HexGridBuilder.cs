using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HexGridBuilder
{
    // https://github.com/Amaranthos/UnityHexGrid/blob/master/HexGrid/Assets/Grid.cs
    public List<HexTileInfo> HexTileInfos { get; private set; }
        = new List<HexTileInfo>();


    private float hexRadius;
    private float padding;

    public HexGridBuilder(int count, float hexRadius, float padding)
    {
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
        var index1d = 0;

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
                if (index1d >= count - 1)
                {
                    break;
                }
                // Move up
                index1d++;
                hexTileInfo = CreateHexTileInfo(x, ++y, index1d);
                HexTileInfos.Add(hexTileInfo);
            }
            for (int k = 0; k < ringIndex; k++)
            {
                if (index1d >= count - 1)
                {
                    break;
                }
                // Move down right
                index1d++;
                hexTileInfo = CreateHexTileInfo(++x, --y, index1d);
                HexTileInfos.Add(hexTileInfo);
            }
            for (int k = 0; k < ringIndex; k++)
            {
                if (index1d >= count - 1)
                {
                    break;
                }
                // Move down
                index1d++;
                hexTileInfo = CreateHexTileInfo(x, --y, index1d);
                HexTileInfos.Add(hexTileInfo);
            }
            for (int k = 0; k < ringIndex; k++)
            {
                if (index1d >= count - 1)
                {
                    break;
                }
                // Move left
                index1d++;
                hexTileInfo = CreateHexTileInfo(--x, y, index1d);
                HexTileInfos.Add(hexTileInfo);
            }
            for (int k = 0; k < ringIndex; k++)
            {
                if (index1d >= count - 1)
                {
                    break;
                }
                // Move up left
                index1d++;
                hexTileInfo = CreateHexTileInfo(--x, ++y, index1d);
                HexTileInfos.Add(hexTileInfo);
            }
            for (int k = 0; k < ringIndex; k++)
            {
                if (index1d >= count - 1)
                {
                    break;
                }
                // Move up
                index1d++;
                hexTileInfo = CreateHexTileInfo(x, ++y, index1d);
                HexTileInfos.Add(hexTileInfo);
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
}
