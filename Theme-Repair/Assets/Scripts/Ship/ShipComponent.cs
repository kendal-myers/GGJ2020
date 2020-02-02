using UnityEngine;


[System.Serializable]
public class ShipComponent
{
    public string name;
    public ShipComponentType type;
    public GameObject prefab;
    public int cost;
    public string description;
}

public enum ShipComponentType
{
    Unspecified = 0,
    CandyCanes = 1,
    Donut = 2,
    Engines = 3,
    Peppermints = 4,
    Gumdrops = 5,
    Lollipop = 6,
}

