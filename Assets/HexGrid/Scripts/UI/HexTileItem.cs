using UnityEngine;
using TMPro;

public class HexTileItem : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer meshRenderer;

    [SerializeField]
    private TMP_Text debugIndexText;

    private float size;
    private HexTile hexTile;

    public void Bind(HexTile hexTile, float size, int index)
    {
        transform.rotation = Quaternion.identity;
        debugIndexText.text = index.ToString();
        this.hexTile = hexTile;
        this.size = size;
        ChangeScale();
        ChangeColor();
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
