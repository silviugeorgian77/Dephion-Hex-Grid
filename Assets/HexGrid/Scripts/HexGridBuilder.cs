using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HexGridBuilder
{
    // https://github.com/Amaranthos/UnityHexGrid/blob/master/HexGrid/Assets/Grid.cs
    public List<HexTileInfo> HexTileInfos { get; private set; }
        = new List<HexTileInfo>();

    public HexGridBuilder(int count, float hexRadius, float padding)
    {
        GenerateHexGrid(count, hexRadius, padding);
    }

    private void GenerateHexGrid(
        int count,
        float hexRadius,
        float padding)
    {
        HexTileInfos.Clear();

        HexTileInfo hexTileInfo;

        int mapSize = Mathf.Max(count, count);

        for (int q = -mapSize; q <= mapSize; q++)
        {
            int r1 = Mathf.Max(-mapSize, -q - mapSize);
            int r2 = Mathf.Min(mapSize, -q + mapSize);
            for (int r = r1; r <= r2; r++)
            {
                hexTileInfo = new HexTileInfo();
                hexTileInfo.position = new Vector3(
                    (hexRadius + padding) * 3.0f / 2.0f * q,
                    0,
                    (hexRadius + padding) * Mathf.Sqrt(3.0f) * (r + q / 2.0f)
                );
                hexTileInfo.index = new Vector3(q, r, -q - r);
                HexTileInfos.Add(hexTileInfo);
            }
        }
    }
}
