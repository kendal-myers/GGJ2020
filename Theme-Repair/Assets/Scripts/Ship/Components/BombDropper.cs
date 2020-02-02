using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDropper : MonoBehaviour
{
    public IntVal cookieTotal;
    private Transform activeBomb;
    private Rigidbody activeRb;
    public GameObject[] explosionPrefabs;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && GetComponentInParent<FlightController>() != null)
        {
            if (activeBomb == null)
                DropBomb();
            else
                ExplodeBomb();
        }
        
    }

    public void ExplodeBomb()
    {
        var pos = activeRb.worldCenterOfMass;
        var fx = Instantiate(explosionPrefabs.Random(), pos, Quaternion.identity);
        fx.transform.localScale = Vector3.one * 5f;
        Destroy(activeBomb.gameObject);
        activeRb = null;
        activeBomb = null;

        var affectedObjects = Physics.OverlapSphere(pos, 25f);
        var objs = new List<ExplodeOnContact>();
        foreach(var obj in affectedObjects)
        {
            if (obj.gameObject.layer == LayerMask.NameToLayer("Asteroid"))
            {
                var exploder = obj.GetComponentInParent<ExplodeOnContact>();
                if (exploder && !objs.Contains(exploder))
                {
                    objs.Add(exploder);
                    if (exploder.gameObject.name.Contains("Good"))
                    {
                        cookieTotal.Value += Random.Range(25, 35);
                    }
                    exploder.Explode((exploder.transform.position - pos).normalized * 50f);
                }
            }
        }
    }

    public void DropBomb()
    {
        if (transform.childCount == 0)
            return;

        activeBomb = transform.GetChild(0);

        activeBomb.parent = null;

        var holder = activeBomb.transform.FindDeepChild("MissleHolder");
        Destroy(holder.gameObject);

        activeRb = activeBomb.gameObject.AddComponent<Rigidbody>();
        activeRb.useGravity = false;
        activeRb.ResetCenterOfMass();
        activeRb.ResetInertiaTensor();
        activeRb.AddForce(transform.root.forward * 50f, ForceMode.VelocityChange);
        var burner = activeBomb.GetComponentInChildren<ParticleSystem>(true);
        burner.gameObject.SetActive(true);
    }
}
