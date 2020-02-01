using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeOnContact : MonoBehaviour
{
    public GameObject explodedPrefab;
    public float explosionForce;
    public float explosionRange;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody.CompareTag("Player") && !collision.collider.isTrigger)
        {
            Explode(collision.rigidbody.velocity);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody.CompareTag("Player") && !other.isTrigger)
        {
            Explode(other.attachedRigidbody.velocity);
        }
    }

    public void Explode(Vector3 collisionVelocity)
    {
        //Destroy(this.GetComponent<Rigidbody>());

        var obj = Instantiate(explodedPrefab, this.transform.position, this.transform.rotation, this.transform.parent);
        foreach (var rb in obj.GetComponentsInChildren<Rigidbody>())
        {
            rb.AddForce(collisionVelocity, ForceMode.VelocityChange);
            rb.AddExplosionForce(explosionForce, this.transform.position, explosionRange);
        }

        Destroy(this.gameObject);
    }
}
