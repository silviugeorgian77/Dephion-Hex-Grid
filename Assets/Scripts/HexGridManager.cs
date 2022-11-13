using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGridManager : MonoBehaviour
{
    [SerializeField]
    private TextAsset hexGridAsset;

    [SerializeField]
    private HexGridDisplayer hexGridDisplayer;

    private IHexGridDataProvider hexGridDataProvider;

    private void Awake()
    {
        hexGridDataProvider = new HexGridDataService(hexGridAsset.text);
        hexGridDataProvider.GetHexGrid(hexGrid =>
        {
            if (hexGrid != null)
            {
                hexGridDisplayer.Bind(hexGrid);
            }
        });
    }
}
