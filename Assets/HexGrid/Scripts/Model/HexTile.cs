using System;
using Newtonsoft.Json;

[Serializable]
public class HexTile
{
    [JsonProperty("Index")]
    public int index;

    [JsonProperty("ClickedColor")]
    public string clickedColor;
}
