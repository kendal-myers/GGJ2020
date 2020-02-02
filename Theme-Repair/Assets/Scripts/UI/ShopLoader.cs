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
    public Sprite[] shopKeepers;
    public string[] shopKeeperText;

    private void Start()
    {
        currentFlightTime = 0f;
    }

    private void Update()
    {
        currentFlightTime += Time.deltaTime;
        if (currentFlightTime > timeUntilShopAppears)
        {
            if (currentShopId >= shopKeepers.Length)
                throw new UnityException("YOU WIN!");
             
            PlaceShop();
            this.enabled = false;
        }
    }

    private GameObject currentShop;
    private void PlaceShop()
    {
        var d = distanceToShop;
        if (currentShopId == shopPrefabs.Length - 1)
            d *= 3;
        currentShop = Instantiate(shopPrefabs[currentShopId], realShip.transform.position + Random.onUnitSphere * d, Quaternion.identity);
        shopTracker.Obj = currentShop;
        shopTracker.gameObject.SetActive(true);
    }

    public IEnumerator LoadShop(int shopId)
    {
        shopTracker.gameObject.SetActive(false);
        yield return StartCoroutine(LoadShopAsync(shopId));
    }

    private IEnumerator LoadShopAsync(int shopId)
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
        Destroy(displayShip.GetComponent<AudioSource>());

        SceneManager.MoveGameObjectToScene(displayShip, SceneManager.GetSceneByName("Shop"));
                
        var shipPedestal = GameObject.Find("Ship Offset");

        displayShip.transform.parent = shipPedestal.transform;

        displayShip.transform.localPosition = Vector3.zero;
        displayShip.transform.localRotation = Quaternion.identity;
        displayShip.transform.localScale = Vector3.one;
        displayShip.transform.Find("candyShip").localScale = Vector3.one;

        var partPicker = GameObject.Find("Part Picker").GetComponent<ShopPartPicker>();
        partPicker.LoadStore(realShip.GetComponent<ShipComponentManager>());

        var pic = GameObject.Find("Shop Keeper Pic").GetComponent<UnityEngine.UI.Image>();
        pic.sprite = shopKeepers[shopId];

        var shopText = GameObject.Find("ShopText").GetComponent<UnityEngine.UI.Text>();
        shopText.text = shopKeeperText[shopId];
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
        Destroy(currentShop);
        currentShopId++;
        currentFlightTime = 0f;
        this.enabled = true;
    }

    public void PurchaseEquip(ShipComponent component)
    {
        realShip.GetComponent<ShipComponentManager>().AddShipComponent(component);
    }
}
