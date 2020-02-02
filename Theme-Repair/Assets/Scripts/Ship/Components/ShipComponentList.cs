using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Custom/Ship Component List")]
public class ShipComponentList : ScriptableObject
{
    public List<ShipComponent> items = new List<ShipComponent>();
}
