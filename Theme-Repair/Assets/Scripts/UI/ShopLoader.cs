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
        SceneManager.LoadScene("Shop", LoadSceneMode.Additive);        
        var shipPedestal = GameObject.Find("Ship Offset");
        displayShip = Instantiate(realShip, shipPedestal.transform);
        displayShip.transform.localPosition = Vector3.zero;
        displayShip.transform.localRotation = Quaternion.identity;        
    }

    public void UnloadShop()
    {
        SceneManager.UnloadSceneAsync("Shop");
    }
}
