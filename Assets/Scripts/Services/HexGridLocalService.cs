
using System;

public class HexGridLocalService : IHexGridLocalProvider
{
    public Action<HexGrid> GetHexGrid()
    {
        throw new NotImplementedException();
    }

    public void SaveHexGrid(Action<bool, HexGrid> onHexGridSaved)
    {
        throw new NotImplementedException();
    }
}
