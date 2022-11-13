
using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

public class HexGridLocalDataService : IHexGridLocalDataProvider
{
    private const string FOLDER_NAME = "HexGrid";
    private const string FILE_NAME = "hexagonGrid.json";

    private readonly string DIRECTORY_PATH;

    private readonly string FILE_PATH;

    public HexGridLocalDataService()
    {
        DIRECTORY_PATH =
            Application.persistentDataPath +
            Path.DirectorySeparatorChar +
            FOLDER_NAME;

        FILE_PATH =
            DIRECTORY_PATH +
            Path.DirectorySeparatorChar +
            FILE_NAME;
    }

    public string GetHexGridDirectoryPath()
    {
        return DIRECTORY_PATH;
    }

    public string GetHexGridFilePath()
    {
        return FILE_PATH;
    }

    public async void GetHexGrid(Action<HexGrid> onHexGridReady)
    {
        HexGrid hexGrid = null;
        await Task.Run(() =>
        {
            try
            {
                if (File.Exists(FILE_PATH))
                {
                    var jsonString = File.ReadAllText(FILE_PATH);
                    hexGrid
                        = JsonConvert.DeserializeObject<HexGrid>(jsonString);
                }
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        });
        onHexGridReady?.Invoke(hexGrid);
    }

    public async void SaveHexGrid(
        HexGrid hexGrid,
        Action<bool, HexGrid> onHexGridSaved)
    {
        bool isSuccess = false;
        await Task.Run(() =>
        {
            try
            {
                if (!Directory.Exists(DIRECTORY_PATH))
                {
                    Directory.CreateDirectory(DIRECTORY_PATH);
                }
                var jsonString = JsonConvert.SerializeObject(hexGrid);
                File.WriteAllText(FILE_PATH, jsonString);
                isSuccess = true;
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
                isSuccess = false;
            }
        });
        onHexGridSaved?.Invoke(isSuccess, hexGrid);
    }
}
