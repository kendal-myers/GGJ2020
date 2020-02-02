using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopEntry : MonoBehaviour
{
    public int shopId;
    private bool entered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger && !entered)
        {
            entered = true;
            var shop = GameObject.FindObjectOfType<ShopLoader>();
            shop.LoadShop(shopId);
        }
    }
}
