using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGridDisplayer : MonoBehaviour
{
    [SerializeField]
    public GameObject hexTilePrefab;

    private HexGrid hexGrid;

    public void Bind(HexGrid hexGrid)
    {
        this.hexGrid = hexGrid;

        // For testing
        hexGrid.tiles.AddRange(hexGrid.tiles);
        hexGrid.tiles.AddRange(hexGrid.tiles);

        GenerateHexGrid();
    }

    private void GenerateHexGrid()
    {
        
    }
}
