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
    private Color oldColor;
    private Color activeColor;
    private float activeColorTimeS;
    private float size;
    private float initY;
    private float initRotationY;
    private bool isSelected;

    private const float APPEAR_MOVE_DELTA_Y = -10f;
    private const float APPEAR_MOVE_DURATION = .2f;
    private const float SELECT_MOVE_DELTA_Y = 1f;
    private const float SELECT_MOVE_DURATION = .5f;
    private const float SELECTED_MOVE_DELTA_Y = .3f;
    private const float SELECTED_MOVE_DURATION = 1f;
    private const float DESELECT_ROTATION_DURATION = .5f;
    private const float SELECTED_ROTATION_DURATION = 4f;
    private const float COLOR_CHANGE_DURATION = 1f;

    private void Awake()
    {
        initRotationY = transform.localRotation.y;
        initY = transform.localPosition.y;

        Hide();
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
        activeColor = defaultColor;
        this.size = size;
        transform.localPosition = hexTileInfo.position;
        initY = transform.localPosition.y;
        ChangeScale();
        ChangeColor(defaultColor);
        touchable.OnClickEndedInsideCallBack = (touchable) =>
        {
            onClickAction?.Invoke(this);
        };
        debugIndexText.text = hexTileInfo.index1d.ToString();
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

    public void Appear()
    {
        if (HexTileInfo != null)
        {
            transform.localPosition = HexTileInfo.position;
        }

        Hide();

        moveable.MoveY(
            initY,
            APPEAR_MOVE_DURATION,
            TransformScope.LOCAL,
            EaseEquations.easeOutCubic
        );
    }

    public void Hide()
    {
        moveable.MoveY(initY + APPEAR_MOVE_DELTA_Y, 0, TransformScope.LOCAL);
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
        if (isSelected)
        {
            return;
        }

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
        oldColor = meshRenderer.material.color;
        activeColor = selectedColor;
        activeColorTimeS = 0;
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
        if (!isSelected)
        {
            return;
        }

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
        oldColor = meshRenderer.material.color;
        activeColor = defaultColor;
        activeColorTimeS = 0;
    }

    private void Update()
    {
        if (activeColorTimeS < COLOR_CHANGE_DURATION)
        {
            activeColorTimeS += Time.deltaTime;
            var color = Color.Lerp(
                oldColor,
                activeColor,
                activeColorTimeS / COLOR_CHANGE_DURATION
            );
            ChangeColor(color);
        }
    }
}
