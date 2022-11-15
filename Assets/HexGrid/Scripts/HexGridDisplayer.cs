using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

public class HexGridDisplayer : MonoBehaviour
{
    [SerializeField]
    public GameObject hexTilePrefab;

    private HexGrid hexGrid;
    private Color defaultTileColor;
    private bool inputEnabled;

    private List<HexTileInfo> hexTileInfos = new List<HexTileInfo>();
    private List<HexTileItem> hexTileItems = new List<HexTileItem>();

    private const int APPEAR_DELAY_PER_TILE_MS = 50;

    public async void Bind(HexGrid hexGrid)
    {
        this.hexGrid = hexGrid;

        inputEnabled = false;

        defaultTileColor
            = ColorUtils.GetColorFromHex(hexGrid.defaultTileColor);

        ClearHexGrid();

        var builder = new HexGridBuilder(
            hexGrid.tiles.Count,
            hexGrid.tileSize / 2f,
            hexGrid.tilePadding
        );
        hexTileInfos = builder.HexTileInfos;

        CreateHexGrid();

        await DisplayHexGrid();
        inputEnabled = true;
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

    private void CreateHexGrid()
    {
        HexTile hexTile;
        HexTileInfo hexTileInfo;
        HexTileItem hexTileItem;
        for (var i = 0; i < hexTileInfos.Count; i++)
        {
            hexTile = hexGrid.tiles[i];
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
        if (!inputEnabled)
        {
            return;
        }

        foreach (var currentHexTileItem in hexTileItems)
        {
            if (currentHexTileItem == hexTileItem)
            {
                currentHexTileItem.ToggleSelected();
            }
            else
            {
                currentHexTileItem.Deselect();
            }
        }
    }

    private async Task DisplayHexGrid()
    {
        foreach (var currentHexTileItem in hexTileItems)
        {
            currentHexTileItem.Appear();
            await Task.Delay(APPEAR_DELAY_PER_TILE_MS);
        }
    }
}
