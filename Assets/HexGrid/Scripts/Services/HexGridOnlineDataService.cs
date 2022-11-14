using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGridOnlineDataService : IHexGridOnlineDataProvider
{
    private const string END_POINT = "https://habticgeneral.blob.core.windows.net/public/dev-cases/hexagonGrid.json";

    private WebClient webClient = new WebClient(new JsonSerializationOption());

    public void GetHexGrid(Action<HexGrid> onHexGridReady)
    {
        webClient.Get<HexGrid>(END_POINT, (hexGrid, result) => {
            onHexGridReady?.Invoke(hexGrid);
        });
    }
}
