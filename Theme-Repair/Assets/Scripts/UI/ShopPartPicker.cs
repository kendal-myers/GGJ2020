using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ShopPartPicker : MonoBehaviour
{
    public ShipComponentList components;
    public PartDisplay[] partStages;
    // Start is called before the first frame update
    public void LoadStore(ShipComponentManager shipManager)
    {
        var part1 = components.items.Where(p => shipManager.CanEquip(p)).Random();
        partStages[0].WireUp(part1, shipManager);
        var part2 = components.items.Where(p => p != part1 && shipManager.CanEquip(p)).Random();
        partStages[1].WireUp(part2, shipManager);
        var part3 = components.items.Where(p => p != part1 && p != part2 && shipManager.CanEquip(p)).Random();
        partStages[2].WireUp(part3, shipManager);
        var part4 = components.items.Where(p => p != part1 && p != part2 && p != part3 && shipManager.CanEquip(p)).Random();
        partStages[3].WireUp(part4, shipManager);
    }

    private void WireUp(int idx, ShipComponent component)
    {
        //var obj = Instantiate(component.prefab, partStages[idx]);
        //Fuck this noise, can't get it to center correctly
        //foreach (var collider in obj.GetComponentsInChildren<Collider>(true))
        //    collider.enabled = true;

        //var rb = obj.GetComponent<Rigidbody>();
        //if (!rb)
        //{
        //    rb = obj.AddComponent<Rigidbody>();            
        //}

        //rb.useGravity = false;
        //rb.ResetCenterOfMass();

        //rb.transform.localPosition = -rb.centerOfMass;

    }
}
