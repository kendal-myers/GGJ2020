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
        rb.AddForce(new Vector3(Input.GetAxis("Horizontal") * turnSpeedH, -Input.GetAxis("Vertical") * turnSpeedV, forwardSpeed), ForceMode.Acceleration);
        rb.rotation = Quaternion.Euler(new Vector3(-rb.velocity.y * turnSpeedV, 0, -rb.velocity.x * turnSpeedH));
    }
}
