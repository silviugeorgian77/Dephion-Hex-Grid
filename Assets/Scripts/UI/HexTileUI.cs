using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTileUI : MonoBehaviour
{
    private HexTile hexTile;

    public void Bind(HexTile hexTile)
    {
        this.hexTile = hexTile;
    }
}
