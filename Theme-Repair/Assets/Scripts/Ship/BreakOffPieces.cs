using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
public class BreakOffPieces : MonoBehaviour
{
    public IntVal cookieTotal;
    public float damageCooldown = .5f;
    public CameraController cameraController;
    public Collider asteroidReplacer;
    public GameObject explosion;
    private float nextDamage = 0f;
   

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Asteroid"))
        {
            if (collision.gameObject.name.Contains("Good"))
            {
                cookieTotal.Value += Random.Range(25, 35);
            } else
            {
                Break();
            }            
        }
    }

    public void Break()
    {
        if (nextDamage <= Time.time)
        {
            var availableComponents = GetComponentsInChildren<BreakableComponent>().OrderBy(c => c.lossPriority).ToList();
            if (availableComponents.Count() == 0)
            {
                StartCoroutine(Explode());
                return;
            }

            availableComponents = availableComponents.Where(c => c.lossPriority == availableComponents[0].lossPriority).ToList();
            var component = availableComponents.Random();
            //Debug.Log($"Just lost {component.name}");
            component.Break();

            nextDamage = Time.time + damageCooldown;
        }
    }

    private IEnumerator Explode()
    {
        FlightController flightController = GetComponent<FlightController>();
        Rigidbody rb = GetComponent<Rigidbody>();
        Destroy(flightController);
        asteroidReplacer.enabled = false;
        rb.mass = 1;
        rb.drag = 0.1f;
        rb.angularDrag = 0.1f;
        cameraController.StopAndStare(true);
        Instantiate(explosion, rb.worldCenterOfMass, transform.rotation, this.transform.parent);
        yield return new WaitForSecondsRealtime(3);
        SceneManager.LoadScene("Lose", LoadSceneMode.Single);
    }
}
