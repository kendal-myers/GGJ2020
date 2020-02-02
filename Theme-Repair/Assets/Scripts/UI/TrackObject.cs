using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackObject : MonoBehaviour
{
    
    public GameObject Obj;

    private void LateUpdate()
    {
        transform.LookAt(Obj.transform);
    }
}
