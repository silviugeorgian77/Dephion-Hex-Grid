using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGridOnlineDataService : IHexGridOnlineDataProvider
{
    public void GetHexGrid(Action<HexGrid> onHexGridReady)
    {
        onHexGridReady?.Invoke(null);
    }
}
