using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ShipComponentManager : MonoBehaviour
{
    public ShipComponentList list;
    public Transform container;
    public Dictionary<ShipComponentType, GameObject> components = new Dictionary<ShipComponentType, GameObject>();

    private void InitComponents()
    {
        AddShipComponent(list.items.First(i => i.type == ShipComponentType.Engines));
        AddShipComponent(list.items.First(i => i.type == ShipComponentType.SmartieBombs));
        AddShipComponent(list.items.First(i => i.type == ShipComponentType.BunnyEars));
        AddShipComponent(list.items.Where(i => !components.Keys.Contains(i.type)).Random());
    }

    private void Update()
    {
        if (components.Count == 0)
            InitComponents();

        this.enabled = false;
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

    public bool CanEquip(ShipComponent component)
    {
        if (!components.ContainsKey(component.type))
            return true;
        else
            return components[component.type].transform.childCount != component.prefab.transform.childCount;
    }
}