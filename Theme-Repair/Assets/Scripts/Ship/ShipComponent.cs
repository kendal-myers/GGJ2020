using UnityEngine;


[System.Serializable]
public class ShipComponent
{
    public ShipComponentType type;
    public GameObject prefab;
    public int cost;
}

public enum ShipComponentType
{
    Unspecified = 0,
    CandyCanePiping = 1,
    Donut = 2,
    Engines = 3,
    Peppermints = 4,
}

