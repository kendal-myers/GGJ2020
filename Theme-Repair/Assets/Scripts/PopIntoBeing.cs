using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopIntoBeing : MonoBehaviour
{
    Vector3 endScale;
    public float timeToGrow = 1f;
    void Start()
    {
        endScale = transform.localScale;
        //Grow();
    }   

    public void Grow()
    {
        StopAllCoroutines();
        StartCoroutine(Embiggen());
    }
    
    private IEnumerator Embiggen()
    {        
        Vector3 startScale = Vector3.zero;
        var progress = 0f;
        while (progress < timeToGrow)
        {
            transform.localScale = Vector3.Lerp(startScale, endScale, Mathf.Clamp01(progress / timeToGrow));
            progress += Time.deltaTime;
            yield return null;
        }

        transform.localScale = endScale;        
    }
}
