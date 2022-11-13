using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGridOnlineDataService : IHexGridOnlineDataProvider
{
    private const string END_POINT = "https://drive.google.com/uc?export=download&id=1MVuV4f8KKGW-zmYJK6sIlaKLy95nm-rC";

    private WebClient webClient = new WebClient(new JsonSerializationOption());

    public void GetHexGrid(Action<HexGrid> onHexGridReady)
    {
        webClient.Get<HexGrid>(END_POINT, (hexGrid, result) => {
            onHexGridReady?.Invoke(hexGrid);
        });
    }
}
