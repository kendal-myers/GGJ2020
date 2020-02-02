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
    public AudioSource source2;
    public AudioClip thrustSound;
    public AudioClip boostSound;

    public float boostChargeCurrent;
    bool isBoosting;
    bool isBoostPressed;
    bool boostOff;
    // Start is called before the first frame update
    void Start()
    {
        rb = ship.GetComponent<Rigidbody>();
        boostChargeCurrent = 0;
        isBoosting = false;
        boostOff = true;
        playIdleThrusterAudio();
        source2 = GetComponents<AudioSource>()[1];
    }

    // Update is called once per frame
    void Update()
    {
        if (!isBoosting)
        {
            boostOff = true;
        }
        else
        {
            if (boostOff)
            {
                boostOff = false;
            }
        }
    }

    private void FixedUpdate()
    {
        float v = Input.GetAxis("Vertical") == 0 ? -transform.worldToLocalMatrix.MultiplyVector(rb.angularVelocity).x * 1f : Input.GetAxis("Vertical") * turnSpeedV;
        float h = Input.GetAxis("Horizontal") == 0 ? -transform.worldToLocalMatrix.MultiplyVector(rb.angularVelocity).z * 1f : -Input.GetAxis("Horizontal") * turnSpeedH;

        bool wasBoosting = isBoosting || isBoostPressed;

        isBoostPressed = Input.GetAxis("Boost") > 0.5f || Input.GetAxis("Boost") < -0.5f;
                
        isBoosting = isBoostPressed && boostChargeCurrent > 5;

        if (!wasBoosting && isBoosting)
        {
            source2.Play();
        }

        float f = rb.velocity.magnitude / 20;
        audioSource.pitch = Mathf.Clamp(f, 1, 3);

        rb.AddRelativeForce(Vector3.forward * forwardSpeed, ForceMode.Acceleration);
        rb.AddRelativeTorque(new Vector3(v, 0, h), ForceMode.Acceleration);
        rb.AddRelativeForce(Vector3.forward * (isBoosting ? boostSpeed : 0), ForceMode.VelocityChange);

        boostChargeCurrent += (isBoosting ? -1 : boostChargeRate);
        boostChargeCurrent = (boostChargeCurrent >= boostChargeMax ? boostChargeMax : boostChargeCurrent);
    }

    void playIdleThrusterAudio()
    {
        audioSource.Stop();
        audioSource.clip = thrustSound;
        audioSource.loop = true;
        audioSource.Play();
    }
}
