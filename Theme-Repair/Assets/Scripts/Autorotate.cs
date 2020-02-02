using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Autorotate : MonoBehaviour
{
    public bool randomStartOffset = false;
    public bool randomSpeed = false;
    public Vector3 rotationSpeed;
    public Space rotationMode = Space.Self;
    public bool useUnscaledTime;

    private void Start()
    {
        if (randomStartOffset)
        {
            transform.Rotate(rotationSpeed * UnityEngine.Random.Range(0f, 30f), rotationMode);
        }

        if (randomSpeed)
        {
            rotationSpeed *= UnityEngine.Random.Range(.25f, 1f);

            if (UnityEngine.Random.value > .5f)            
                rotationSpeed = -rotationSpeed;
        }
    }
    private void Update()
    {
        if (useUnscaledTime)
            transform.Rotate(rotationSpeed * Time.unscaledDeltaTime, rotationMode);
        else
            transform.Rotate(rotationSpeed * Time.deltaTime, rotationMode);
    }
}
