using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabExplosion : MonoBehaviour
{
    public Seeker prefab;
    public int explosionCount;
    public float explosionForce;

    private void Start()
    {
        var shipT = GameObject.Find("Ship (1)").transform;
        for (int i = 0; i < explosionCount; i++)
        {
            var dir = Random.onUnitSphere;
            while (Vector3.Dot(Camera.main.transform.forward, dir) < .5f)
                dir = Random.onUnitSphere;

            var obj = Instantiate(prefab, this.transform.position + dir * .25f, Random.rotation);
            obj.velocity = dir * explosionForce;
            obj.target = shipT;
        }
        Destroy(this.gameObject);
    }
}
