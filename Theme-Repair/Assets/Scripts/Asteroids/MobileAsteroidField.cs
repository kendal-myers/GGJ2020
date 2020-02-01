using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MobileAsteroidField : MonoBehaviour
{
    public static MobileAsteroidField Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            throw new UnityException("Two asteroid fields were loaded at once. Bad!");
    }
    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }
    public Transform container;
    public PopIntoBeing[] asteroidPrefabs;
    public int density;
    public float minRange;
    public float maxRange;
    public Vector3 drift = Vector3.one;

    private void Start()
    {
        for(int i = 0; i < density; i++)
        {
            var position = Random.onUnitSphere * minRange + Random.insideUnitSphere * (maxRange - minRange);
            var asteroid = Instantiate(asteroidPrefabs.Random(), position, Random.rotation, container);
            asteroid.GetComponent<Rigidbody>()?.AddForceAtPosition(drift.Random(true), Random.onUnitSphere, ForceMode.Impulse);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Reposition(other.transform);
    }

    public void Reposition(Transform asteroid)
    {
        var newPos = Random.onUnitSphere * maxRange;
        while (Vector3.Dot(transform.forward, newPos) < 0)
            newPos = Random.onUnitSphere * maxRange;

        //Debug.DrawRay(transform.position, newPos, Color.red, .25f);        

        asteroid.SetPositionAndRotation(transform.position + newPos, Random.rotation);
        asteroid.GetComponent<PopIntoBeing>()?.Grow();        
    }
}
