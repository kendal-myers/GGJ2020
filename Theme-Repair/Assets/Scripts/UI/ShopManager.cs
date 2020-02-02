using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public void ExitShop()
    {
        var loader = GameObject.FindObjectOfType<ShopLoader>();
        loader.UnloadShop();
    }
}
