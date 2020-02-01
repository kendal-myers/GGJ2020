using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightController : MonoBehaviour
{
    public GameObject ship;
    private Rigidbody rb;
    public float forwardSpeed;
    public float turnSpeedV;
    public float turnSpeedH;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = ship.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        float v = Input.GetAxis("Vertical") == 0 ? -transform.worldToLocalMatrix.MultiplyVector(rb.angularVelocity).x * 1f : Input.GetAxis("Vertical") * turnSpeedV;
        float h = Input.GetAxis("Horizontal") == 0 ? -transform.worldToLocalMatrix.MultiplyVector(rb.angularVelocity).z * 1f : -Input.GetAxis("Horizontal") * turnSpeedH;

        rb.AddRelativeForce(Vector3.forward * forwardSpeed, ForceMode.Acceleration);
        rb.AddRelativeTorque(new Vector3(v, 0, h), ForceMode.Acceleration);
    }
}
