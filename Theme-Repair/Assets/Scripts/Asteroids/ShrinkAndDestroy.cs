using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ShrinkAndDestroy : MonoBehaviour
{
    public Transform[] pieces;
    public float lifetime;
    public float progress;
    Vector3[] startScales;
    private void Start()
    {
        if (pieces == null || pieces.Length == 0)
            pieces = new Transform[1] { this.transform };
        startScales = pieces.Select(p => p.transform.localScale).ToArray();
    }

    private void Update()
    {
        progress += Time.deltaTime;
        for (int i = 0; i < pieces.Length; i++)
            pieces[i].localScale = Vector3.Lerp(startScales[i], Vector3.zero, Mathf.Clamp01(progress / lifetime));

        if (progress >= lifetime)
            Destroy(this.gameObject);            
    }
}
