using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableComponent: MonoBehaviour
{
    public int lossPriority;
    public float mass;
    private Collider myCollider;
    private ShrinkAndDestroy terminator;
    private float breakForce = 2.5f;
    public GameObject[] fxPrefabs;

    private void Awake()
    {
        myCollider = GetComponentInChildren<Collider>();
        terminator = GetComponent<ShrinkAndDestroy>();
    }

    public void Break()
    {
        var parentRB = GetComponentInParent<Rigidbody>();
        var rb = this.gameObject.AddComponent<Rigidbody>();
        rb.velocity = parentRB.velocity;        
        rb.useGravity = false;
        rb.mass = mass;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.ResetCenterOfMass();
        rb.AddForce((rb.centerOfMass - transform.position).normalized * breakForce, ForceMode.VelocityChange);
        myCollider.enabled = true;

        Instantiate(fxPrefabs.Random(), rb.worldCenterOfMass, transform.rotation, this.transform.parent);

        this.transform.parent = null;

        terminator.enabled = true;        
    }
}
