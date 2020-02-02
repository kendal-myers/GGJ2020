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
    public float boostSpeed;
    public float boostChargeMax;
    public float boostChargeRate;

    public Sounds sounds;
    public AudioSource audioSource;
    public float boostChargeCurrent;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = ship.GetComponent<Rigidbody>();
        boostChargeCurrent = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        float v = Input.GetAxis("Vertical") == 0 ? -transform.worldToLocalMatrix.MultiplyVector(rb.angularVelocity).x * 1f : Input.GetAxis("Vertical") * turnSpeedV;
        float h = Input.GetAxis("Horizontal") == 0 ? -transform.worldToLocalMatrix.MultiplyVector(rb.angularVelocity).z * 1f : -Input.GetAxis("Horizontal") * turnSpeedH;

        bool isBoostPressed = Input.GetAxis("Boost") > 0.5f || Input.GetAxis("Boost") < -0.5f;
        bool isBoosting = isBoostPressed && boostChargeCurrent > 5;

        rb.AddRelativeForce(Vector3.forward * forwardSpeed, ForceMode.Acceleration);
        rb.AddRelativeTorque(new Vector3(v, 0, h), ForceMode.Acceleration);
        rb.AddRelativeForce(Vector3.forward * (isBoosting ? boostSpeed : 0), ForceMode.VelocityChange);

        boostChargeCurrent += (isBoosting ? -1 : boostChargeRate);
        boostChargeCurrent = (boostChargeCurrent >= boostChargeMax ? boostChargeMax : boostChargeCurrent);
    }
}
