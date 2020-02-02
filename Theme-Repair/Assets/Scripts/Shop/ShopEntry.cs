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
            var ship = other.gameObject.GetComponentInParent<FlightController>();
            if (ship)
                StartCoroutine(DockTheShip(ship.gameObject));
            else
                Debug.Log($"Just collided with {other.name} which isn't a ship");
        }
    }

    private IEnumerator DockTheShip(GameObject ship)
    {
        entered = true;
        if (isEndOfGame)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Win");
            yield break;
        }

        ship.GetComponent<FlightController>().enabled = false;
        Camera.main.GetComponent<CameraController>().enabled = false;
        Vector3 velocity = Vector3.zero;
        
        while ((ship.transform.position - this.transform.position).magnitude > 2f)
        {
            var dist = (ship.transform.position - this.transform.position).magnitude;
            ship.transform.position = Vector3.SmoothDamp(ship.transform.position, transform.position, ref velocity, .3f, 8f, Time.deltaTime);
            ship.transform.rotation = Quaternion.Lerp(ship.transform.rotation, Quaternion.LookRotation(transform.position - ship.transform.position, ship.transform.up), .05f);

            ship.transform.Find("candyShip").localScale = Vector3.Lerp(Vector3.zero, Vector3.one, Mathf.Clamp01((dist - 2f) / 4f));

            Camera.main.transform.LookAt(ship.transform.position);
            yield return null;
        }

        ship.transform.localScale = Vector3.one;
        ship.GetComponent<FlightController>().enabled = true;
        Camera.main.GetComponent<CameraController>().enabled = true;

        var shop = GameObject.FindObjectOfType<ShopLoader>();
        yield return shop.LoadShop(shopId);
        yield return new WaitForSeconds(2f);
        ship.transform.Find("candyShip").localScale = Vector3.one;
        ship.GetComponent<FlightController>().enabled = true;
        Camera.main.GetComponent<CameraController>().enabled = true;
    }
}
