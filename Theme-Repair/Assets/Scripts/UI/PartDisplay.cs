using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartDisplay : MonoBehaviour
{
    public IntVal cookieTotal;
    public Transform pedestal;
    public Text cost;
    public Text partName;
    public Text partDescription;
    public Image disableOverlay;
    public Text buyText;
    private ShipComponent component;
    public Transform shipPedestal;
    private ShipComponentManager realShip;
    private bool isPurchased = false;

    private void Start()
    {
        cookieTotal.ValueChanged += CookieTotal_ValueChanged;
    }

    private void CookieTotal_ValueChanged()
    {
        if (component != null && !isPurchased)
            disableOverlay.gameObject.SetActive(cookieTotal.Value < component.cost);        
    }

    private void OnDestroy()
    {
        cookieTotal.ValueChanged -= CookieTotal_ValueChanged;
    }

    public void WireUp(ShipComponent component, ShipComponentManager realShip)
    {
        var obj = Instantiate(component.prefab, pedestal);
        cost.text = component.cost.ToString();
        partName.text = component.name;
        this.component = component;
        this.realShip = realShip;
        CookieTotal_ValueChanged();
        buyText.gameObject.SetActive(false);
    }

    public void HoverEffects()
    {
        if (partDescription.text != component.description)
        {
            partDescription.text = component.description;
            
            UpdateShipDisplay();
        }
        if (!disableOverlay.gameObject.activeSelf)
            buyText.gameObject.SetActive(true);
    }

    public void HoverExit()
    {
        buyText.gameObject.SetActive(false);
    }

    public void UpdateShipDisplay()
    {
        Destroy(shipPedestal.GetComponentInChildren<ShipComponentManager>().gameObject);
        var displayShip = Instantiate(realShip.gameObject, shipPedestal).transform;
        displayShip.gameObject.SetActive(true);
        displayShip.localPosition = Vector3.zero;
        displayShip.localRotation = Quaternion.identity;
        Destroy(displayShip.GetComponent<FlightController>());
        displayShip.GetComponent<ShipComponentManager>().AddShipComponent(component);
    }

    public void Purchase()
    {
        if (realShip.CanEquip(component))
        {
            isPurchased = true;
            realShip.AddShipComponent(component);
            buyText.text = "Purchased";
            cookieTotal.Value -= component.cost;                        
        }
    }
}
