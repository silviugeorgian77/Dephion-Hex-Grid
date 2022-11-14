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
    private Moveable moveable;

    [SerializeField]
    private Rotatable rotatable;

    [SerializeField]
    private TMP_Text debugIndexText;

    public HexTile HexTile { get; private set; }
    public HexTileInfo HexTileInfo { get; private set; }

    private Color defaultColor;
    private Color selectedColor;
    private float size;
    private float initY;
    private float initRotationY;
    private bool isSelected;

    private const float SELECT_MOVE_DELTA_Y = 1f;
    private const float SELECT_MOVE_DURATION = .5f;
    private const float SELECTED_MOVE_DELTA_Y = .3f;
    private const float SELECTED_MOVE_DURATION = 1f;
    private const float DESELECT_ROTATION_DURATION = .5f;
    private const float SELECTED_ROTATION_DURATION = 4f;

    private void Awake()
    {
        initRotationY = transform.localRotation.y;
    }

    public void Bind(
        HexTile hexTile,
        HexTileInfo hexTileInfo,
        Color defaultColor,
        float size,
        Action<HexTileItem> onClickAction)
    {
        HexTile = hexTile;
        HexTileInfo = hexTileInfo;
        this.defaultColor = defaultColor;
        selectedColor = ColorUtils.GetColorFromHex(hexTile.clickedColor);
        this.size = size;
        transform.localPosition = hexTileInfo.position;
        initY = transform.localPosition.y;
        ChangeScale();
        ChangeColor(defaultColor);
        touchable.OnClickEndedInsideCallBack = (touchable) =>
        {
            onClickAction?.Invoke(this);
        };
        debugIndexText.text = hexTileInfo.index.ToString();
    }

    private void ChangeScale()
    {
        float scale = size / meshRenderer.bounds.size.x;
        transform.localScale = new Vector3(scale, scale, scale);
    }

    private void ChangeColor(Color color)
    {
        foreach (var material in meshRenderer.materials)
        {
            material.color = color;
        }
    }

    public void ToggleSelected()
    {
        if (isSelected)
        {
            Deselect();
        }
        else
        {
            Select();
        }
    }

    public void Select()
    {
        isSelected = true;
        moveable.MoveY(
            initY + SELECT_MOVE_DELTA_Y,
            SELECT_MOVE_DURATION,
            TransformScope.LOCAL,
            EaseEquations.easeOut,
            () =>
            {
                FloatDown();
            }
        );
        Spin();
        ChangeColor(selectedColor);
    }

    private void FloatDown()
    {
        moveable.MoveY(
            transform.position.y - SELECTED_MOVE_DELTA_Y,
            SELECTED_MOVE_DURATION,
            TransformScope.LOCAL,
            EaseEquations.easeIn,
            () =>
            {
                FloatUp();
            }
        );
    }

    private void FloatUp()
    {
        moveable.MoveY(
            transform.position.y + SELECTED_MOVE_DELTA_Y,
            SELECTED_MOVE_DURATION,
            TransformScope.LOCAL,
            EaseEquations.easeOut,
            () =>
            {
                FloatDown();
            }
        );
    }

    private void Spin()
    {
        rotatable.RotateByY(
            360,
            SELECTED_ROTATION_DURATION,
            EndCallBack: () =>
            {
                Spin();
            }
        );
    }

    public void Deselect()
    {
        isSelected = false;
        moveable.MoveY(
            initY,
            SELECT_MOVE_DURATION,
            TransformScope.LOCAL,
            EaseEquations.easeIn
        );
        rotatable.RotateToY(
            initRotationY,
            DESELECT_ROTATION_DURATION
        );
        ChangeColor(defaultColor);
    }
}
