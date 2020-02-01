using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MobileAsteroidField : MonoBehaviour
{
    public Transform container;
    public PopIntoBeing[] asteroidPrefabs;
    public int density;
    public float minRange;
    public float maxRange;
    
    private void FixedUpdate()
    {   
        Debug.Log($"Asteroid count: {container.childCount}");
    }

    private void Start()
    {
        for(int i = 0; i < density; i++)
        {
            var position = Random.onUnitSphere * minRange + Random.insideUnitSphere * (maxRange - minRange);
            Instantiate(asteroidPrefabs.Random(), position, Random.rotation, container);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var newPos = Random.onUnitSphere * maxRange;
        while (Vector3.Dot(transform.forward, newPos) < 0)
            newPos = Random.onUnitSphere * maxRange;
        
        Debug.DrawRay(transform.position, newPos, Color.red, .25f);        

        other.transform.SetPositionAndRotation(transform.position + newPos, Random.rotation);
        other.GetComponent<PopIntoBeing>()?.Grow();
    }
}
