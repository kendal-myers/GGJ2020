using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopEntry : MonoBehaviour
{
    public int shopId;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger)
        {
            var shop = GameObject.FindObjectOfType<ShopLoader>();
            shop.LoadShop(shopId);
        }
    }
}
