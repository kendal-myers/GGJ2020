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
            Destroy(this.gameObject);
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
        BuildAsteroidField();
    }

    public void BuildAsteroidField()
    {
        for (int i = 0; i < density; i++)
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
        StartCoroutine(FixTail(asteroid));

        var newPos = Random.onUnitSphere * maxRange;
        while (Vector3.Dot(transform.forward, newPos) < 0)
            newPos = Random.onUnitSphere * maxRange;

        //Debug.DrawRay(transform.position, newPos, Color.red, .25f);        

        asteroid.SetPositionAndRotation(transform.position + newPos, transform.rotation);
        asteroid.GetComponent<PopIntoBeing>()?.Grow();        
    }

    private IEnumerator FixTail(Transform asteroid)
    {
        var tails = asteroid.GetComponentsInChildren<FIMSpace.FTail.FTail_Animator>();
        if (tails == null || tails.Length == 0)
            yield break;
        for(int i = 0; i < tails.Length; i++)
            tails[i].enabled = false;
        yield return new WaitForSeconds(.5f);
        for (int i = 0; i < tails.Length; i++)
            tails[i].enabled = true;
    }

    public void UnloadAsteroids()
    {
        StopAllCoroutines();
        foreach (Transform asteroid in container)
            if (asteroid != container)
                Destroy(asteroid.gameObject);
    }
}
