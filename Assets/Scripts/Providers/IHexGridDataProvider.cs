using System;

public interface IHexGridDataProvider
{
    void GetHexGrid(Action<HexGrid> onHexGrid);
}
