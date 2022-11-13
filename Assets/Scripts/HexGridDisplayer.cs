using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGridDisplayer : MonoBehaviour
{
    private HexGrid hexGrid;

    public void Bind(HexGrid hexGrid)
    {
        this.hexGrid = hexGrid;
    }
}
