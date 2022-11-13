using System;

public interface IHexGridOnlineDataProvider
{
    void GetHexGrid(Action<HexGrid> onHexGridReady);
}
