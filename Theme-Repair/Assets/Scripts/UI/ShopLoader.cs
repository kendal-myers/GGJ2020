using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopLoader : MonoBehaviour
{
    public GameObject realShip;
    public GameObject displayShip;
    public void LoadShop(int shopId)
    {
        StartCoroutine(LoadShopAsync());
    }

    private IEnumerator LoadShopAsync()
    {
        MobileAsteroidField.Instance.SleepAsteroids();

        var async = SceneManager.LoadSceneAsync("Shop", LoadSceneMode.Additive);
        async.allowSceneActivation = true;
        while (async.progress < 1f)
            yield return null;

        displayShip = Instantiate(realShip);

        realShip.SetActive(false);

        Destroy(displayShip.GetComponent<FlightController>());
        
        SceneManager.MoveGameObjectToScene(displayShip, SceneManager.GetSceneByName("Shop"));
                
        var shipPedestal = GameObject.Find("Ship Offset");

        displayShip.transform.parent = shipPedestal.transform;

        displayShip.transform.localPosition = Vector3.zero;
        displayShip.transform.localRotation = Quaternion.identity;

        var partPicker = GameObject.Find("Part Picker").GetComponent<ShopPartPicker>();
        partPicker.LoadStore(realShip.GetComponent<ShipComponentManager>());
    }

    public void UnloadShop()
    {
        MobileAsteroidField.Instance.SleepAsteroids();

        SceneManager.UnloadSceneAsync("Shop");

        realShip.SetActive(true);
        realShip.transform.position = Vector3.zero;
        realShip.transform.rotation = Quaternion.identity;
    }

    public void PurchaseEquip(ShipComponent component)
    {
        realShip.GetComponent<ShipComponentManager>().AddShipComponent(component);
    }
}
