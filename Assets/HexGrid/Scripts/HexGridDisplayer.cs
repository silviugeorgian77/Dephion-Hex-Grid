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

        var builder = new HexGridBuilder(hexGrid.tiles.Count);
        hexTileInfos = builder.HexTileInfos;
        //DisplayHexGrid();

        var a = CreateHexTileItem(0);
        a.transform.position = Vector3.left * 2;
        hexTileItems.Add(a);
        var b = CreateHexTileItem(1);
        hexTileItems.Add(b);
        b.transform.position = Vector3.right * 2;

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
        var distance1 = hexGrid.tileSize + hexGrid.tilePadding;
        var distance2 = distance1;
        for (int i = 0; i < hexGrid.tiles.Count; i++)
        {
            var hexTileItem = CreateHexTileItem(i);
            hexTileItems.Add(hexTileItem);

            if (i == 0)
            {
                hexTileItem.transform.localPosition = Vector3.zero;
            }
            else
            {
                Vector3 deltaPosition;
                var direction
                    = hexTileItem.HexTileInfo.neighbours.Keys.ElementAt(0);
                var neighbour = hexTileItem.HexTileInfo.neighbours[direction];
                switch (direction)
                {
                    case HexDirection.TOP:
                        deltaPosition = new Vector3(
                            0,
                            0,
                            distance1
                        );
                        break;
                    case HexDirection.TOP_RIGHT:
                        deltaPosition = new Vector3(
                            distance2,
                            0,
                            distance2
                        );
                        break;
                    case HexDirection.BOTTOM_RIGHT:
                        deltaPosition = new Vector3(
                            distance2,
                            0,
                            -distance2
                        );
                        break;
                    case HexDirection.BOTTOM:
                        deltaPosition = new Vector3(
                            0,
                            0,
                            -distance2
                        );
                        break;
                    case HexDirection.BOTTOM_LEFT:
                        deltaPosition = new Vector3(
                           -distance2,
                           0,
                           -distance2
                        );
                        break;
                    default:
                        deltaPosition = new Vector3(
                           -distance2,
                           0,
                           distance2
                       );
                        break;
                }
                hexTileItem.transform.localPosition
                    = hexTileItems[neighbour.index].transform.position
                    + deltaPosition;
            }
        }
    }
  
    private HexTileItem CreateHexTileItem(
        int index)
    {
        var hexTile = hexGrid.tiles[index];
        var hexTileInfo = new HexTileInfo() { index = index };
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
                Debug.Log(currentHexTileItem.HexTileInfo.index);
                currentHexTileItem.ToggleSelected();
            }
            else
            {
                currentHexTileItem.Deselect();
            }
        }
    }
}
