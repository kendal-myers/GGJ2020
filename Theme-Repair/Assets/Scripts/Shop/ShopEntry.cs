using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopEntry : MonoBehaviour
{
    public int shopId;
    private bool entered = false;
    public bool isEndOfGame = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger && !entered)
        {
            entered = true;
            if (isEndOfGame)
                throw new UnityException("YOU WIN! GAME OVER!");

            var shop = GameObject.FindObjectOfType<ShopLoader>();
            shop.LoadShop(shopId);
        }
    }
}
