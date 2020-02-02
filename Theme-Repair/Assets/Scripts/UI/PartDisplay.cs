using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartDisplay : MonoBehaviour
{
    public Transform pedestal;
    public Text cost;
    public Text partName;
    public Text partDescription;
    private ShipComponent component;
    public Transform shipPedestal;
    private GameObject realShip;

    public void WireUp(ShipComponent component, GameObject realShip)
    {
        var obj = Instantiate(component.prefab, pedestal);
        cost.text = component.cost.ToString();
        partName.text = component.name;
        this.component = component;
        this.realShip = realShip;
    }

    public void HoverEffects()
    {
        if (partDescription.text != component.description)
        {
            partDescription.text = component.description;

            Destroy(shipPedestal.GetComponentInChildren<ShipComponentManager>().gameObject);
            var displayShip = Instantiate(realShip, shipPedestal).transform;
            displayShip.gameObject.SetActive(true);
            displayShip.localPosition = Vector3.zero;
            displayShip.localRotation = Quaternion.identity;
            Destroy(displayShip.GetComponent<FlightController>());            
            displayShip.GetComponent<ShipComponentManager>().AddShipComponent(component);
        }
    }
}
