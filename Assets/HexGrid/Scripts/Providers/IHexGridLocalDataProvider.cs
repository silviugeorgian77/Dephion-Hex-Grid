using System;

public interface IHexGridLocalDataProvider
{
    string GetHexGridDirectoryPath();
    string GetHexGridFilePath();
    void GetHexGrid(Action<HexGrid> onHexGridReady);
    void SaveHexGrid(HexGrid hexGrid, Action<bool, HexGrid> onHexGridSaved);
}
