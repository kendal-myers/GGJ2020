using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeker : MonoBehaviour
{
    public Transform target;
    public Vector3 velocity;
    public float seekForce = 2f;
    public float seekDelay = 1.25f;
    public float seekSpeed = 1f;

    private void Update()
    {
        this.transform.position += velocity * Time.deltaTime;
        seekDelay -= Time.deltaTime;
        if (seekDelay < 0f)
        {
            var desired = target.position - transform.position * seekForce * Time.deltaTime;
            velocity = Vector3.MoveTowards(velocity, desired, seekSpeed * Time.deltaTime);

            velocity += (target.position - transform.position) * seekForce * Time.deltaTime;
        }
    }
}
