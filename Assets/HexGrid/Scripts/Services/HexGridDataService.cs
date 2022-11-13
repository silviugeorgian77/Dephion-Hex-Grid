using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public class HexGridDataService : IHexGridDataProvider
{
    private IHexGridLocalDataProvider localDataProvider;
    private IHexGridOnlineDataProvider onlineDataProvider;

    private bool isInitialized;

    public HexGridDataService(string defaultHexGridJson)
    {
        localDataProvider = new HexGridLocalDataService();
        onlineDataProvider = new HexGridOnlineDataService();
        ManageDefaultHexGridData(defaultHexGridJson);
    }

    private async void ManageDefaultHexGridData(string defaultHexGridJson)
    {
        await Task.Run(() =>
        {
            var filePath = localDataProvider.GetHexGridFilePath();
            if (!File.Exists(filePath))
            {
                var directoryPath = localDataProvider.GetHexGridDirectoryPath();
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                Debug.Log(
                    nameof(HexGridDataService) +
                    " Default HexGrid json not copied to persistentDataPath"
                );
                File.WriteAllText(filePath, defaultHexGridJson);
                Debug.Log(
                    nameof(HexGridDataService) +
                    " Default HexGrid json copied successful to " +
                    "persistentDataPath"
                );
            }
            else
            {
                Debug.Log(
                    nameof(HexGridDataService) +
                    " HexGrid local data exists"
                );
            }
            isInitialized = true;
        });
    }

    public async void GetHexGrid(Action<HexGrid> onHexGrid)
    {
        while (!isInitialized)
        {
            await Task.Yield();
        }
        onlineDataProvider.GetHexGrid(hexGrid =>
        {
            if (hexGrid != null)
            {
                Debug.Log(
                    nameof(HexGridDataService) +
                    " Online HexGrid obtained"
                );
                localDataProvider.SaveHexGrid(
                    hexGrid,
                    (isCacheSuccessful, hexGrid) =>
                    {
                        Debug.Log(
                            nameof(HexGridDataService) +
                            " Cache successful: " +
                            isCacheSuccessful
                        );
                    }
                );
                onHexGrid?.Invoke(hexGrid);
            }
            else
            {
                Debug.Log(
                    nameof(HexGridDataService) +
                    " No Online HexGrid data"
                );

                localDataProvider.GetHexGrid(hexGrid =>
                {
                    if (hexGrid != null)
                    {
                        Debug.Log(
                            nameof(HexGridDataService) +
                            " Local HexGrid obtained"
                        );
                    }
                    else
                    {
                        Debug.Log(
                            nameof(HexGridDataService) +
                            " No local HexGrid data"
                        );
                    }
                    onHexGrid?.Invoke(hexGrid);
                });
            }
        });
    }
}
