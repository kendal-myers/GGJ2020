using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ShipComponentManager : MonoBehaviour
{
    public ShipComponentList list;
    public Transform container;
    public Dictionary<ShipComponentType, GameObject> components = new Dictionary<ShipComponentType, GameObject>();

    private void Start()
    {
        AddShipComponent(list.items.First(i => i.type == ShipComponentType.Engines));
        AddShipComponent(list.items.Where(i => !components.Keys.Contains(i.type)).Random());
    }

    public void AddShipComponent(ShipComponent component)
    {
        if (components.ContainsKey(component.type))
        {
            Destroy(components[component.type]);
            components.Remove(component.type);
        }

        var item = Instantiate(component.prefab, container);
        components.Add(component.type, item);
    }
}