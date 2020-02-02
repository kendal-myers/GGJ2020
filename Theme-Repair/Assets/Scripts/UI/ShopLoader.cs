using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopLoader : MonoBehaviour
{
    public float currentFlightTime;
    public float timeUntilShopAppears;
    public float distanceToShop = 200f;
    public int currentShopId = 0;
    public GameObject[] shopPrefabs;
    public GameObject realShip;
    public GameObject[] hideInShop;
    public GameObject displayShip;
    public TrackObject shopTracker;

    private void Start()
    {
        currentFlightTime = 0f;
    }

    private void Update()
    {
        currentFlightTime += Time.deltaTime;
        if (currentFlightTime > timeUntilShopAppears)
        {
            PlaceShop();
            this.enabled = false;
        }
    }

    private void PlaceShop()
    {
        var obj = Instantiate(shopPrefabs[currentShopId], Random.onUnitSphere * distanceToShop, Quaternion.identity);
        shopTracker.Obj = obj;
        shopTracker.gameObject.SetActive(true);
    }

    public void LoadShop(int shopId)
    {
        shopTracker.gameObject.SetActive(false);
        StartCoroutine(LoadShopAsync());
    }

    private IEnumerator LoadShopAsync()
    {
        MobileAsteroidField.Instance.UnloadAsteroids();

        foreach(var obj in hideInShop)
            obj.SetActive(false);
        var async = SceneManager.LoadSceneAsync("Shop", LoadSceneMode.Additive);
        async.allowSceneActivation = true;
        while (async.progress < 1f)
            yield return null;

        displayShip = Instantiate(realShip);

        realShip.SetActive(false);
        foreach (var ps in realShip.GetComponentsInChildren<ParticleSystem>())
            ps.Stop();

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
        SceneManager.UnloadSceneAsync("Shop");

        realShip.SetActive(true);
        realShip.transform.position = Vector3.zero;
        realShip.transform.rotation = Quaternion.identity;

        foreach (var ps in realShip.GetComponentsInChildren<ParticleSystem>())
            ps.Play();

        foreach(var obj in hideInShop)
            obj.SetActive(true);

        MobileAsteroidField.Instance.BuildAsteroidField();

        //Start flying towards the next shop
        currentShopId++;
        currentFlightTime = 0f;
        this.enabled = true;
    }

    public void PurchaseEquip(ShipComponent component)
    {
        realShip.GetComponent<ShipComponentManager>().AddShipComponent(component);
    }
}
