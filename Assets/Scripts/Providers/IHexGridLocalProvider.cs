using System;

public interface IHexGridLocalProvider
{
    public Action<HexGrid> GetHexGrid();
    public void SaveHexGrid(Action<bool, HexGrid> onHexGridSaved);
}
