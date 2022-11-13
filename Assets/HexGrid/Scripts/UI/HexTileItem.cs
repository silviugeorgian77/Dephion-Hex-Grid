using UnityEngine;
using TMPro;
using System;

public class HexTileItem : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer meshRenderer;

    [SerializeField]
    private Touchable touchable;

    [SerializeField]
    private TMP_Text debugIndexText;

    public HexTile HexTile { get; private set; }
    public HexTileInfo HexTileInfo { get; private set; }

    private float size;

    public void Bind(
        HexTile hexTile,
        HexTileInfo hexTileInfo,
        float size,
        Action<HexTileInfo> onClickAction)
    {
        HexTile = hexTile;
        HexTileInfo = hexTileInfo;
        this.size = size;
        ChangeScale();
        ChangeColor();
        touchable.OnClickEndedInsideCallBack = (touchable) =>
        {
            onClickAction?.Invoke(HexTileInfo);
        };
        debugIndexText.text = hexTileInfo.index.ToString();
    }

    private void ChangeScale()
    {
        float scale = size / meshRenderer.bounds.size.x;
        transform.localScale = new Vector3(scale, scale, scale);
    }

    private void ChangeColor()
    {
        meshRenderer.sharedMaterial.color = Color.red;
    }
}
