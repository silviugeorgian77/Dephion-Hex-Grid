using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class HexGridDisplayer : MonoBehaviour
{
    [SerializeField]
    public GameObject hexTilePrefab;

    private HexGrid hexGrid;
    private Color defaultTileColor;

    private List<HexTileInfo> hexTileInfos = new List<HexTileInfo>();
    private List<HexTileItem> hexTileItems = new List<HexTileItem>();

    public void Bind(HexGrid hexGrid)
    {
        this.hexGrid = hexGrid;

        defaultTileColor
            = ColorUtils.GetColorFromHex(hexGrid.defaultTileColor);

        // For testing
        //hexGrid.tiles.AddRange(hexGrid.tiles);
        //hexGrid.tiles.AddRange(hexGrid.tiles);

        ClearHexGrid();

        var builder = new HexGridBuilder(
            (int) Mathf.Sqrt(hexGrid.tiles.Count),
            hexGrid.tileSize / 2f,
            hexGrid.tilePadding
        );
        hexTileInfos = builder.HexTileInfos;

        DisplayHexGrid();
    }

    private void ClearHexGrid()
    {
        foreach (var hexTileItem in hexTileItems)
        {
            Destroy(hexTileItem);
        }
        hexTileItems.Clear();
        hexTileInfos.Clear();
    }

    private void DisplayHexGrid()
    {
        HexTile hexTile;
        HexTileInfo hexTileInfo;
        HexTileItem hexTileItem;
        for (var i = 0; i < hexTileInfos.Count; i++)
        {
            hexTile = hexGrid.tiles[0];
            hexTileInfo = hexTileInfos[i];
            hexTileItem = CreateHexTileItem(hexTile, hexTileInfo);
            hexTileItems.Add(hexTileItem);
        }
    }
  
    private HexTileItem CreateHexTileItem(
        HexTile hexTile,
        HexTileInfo hexTileInfo)
    {
        var hexTileObject = Instantiate(hexTilePrefab, transform);
        var hexTileItem = hexTileObject.GetComponent<HexTileItem>();
        hexTileItem.Bind(
            hexTile,
            hexTileInfo,
            defaultTileColor,
            hexGrid.tileSize,
            OnHexTileClicked
        );
        return hexTileItem;
    }

    private void OnHexTileClicked(HexTileItem hexTileItem)
    {
        Debug.Log("OnHexTileClicked");
        foreach (var currentHexTileItem in hexTileItems)
        {
            if (currentHexTileItem == hexTileItem)
            {
                Debug.Log(currentHexTileItem.HexTileInfo.position);
                currentHexTileItem.ToggleSelected();
            }
            else
            {
                currentHexTileItem.Deselect();
            }
        }
    }
}
