using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakOffPieces : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Asteroid"))
        {
            Debug.Log("A piece breaks off!");
        }
    }
}
