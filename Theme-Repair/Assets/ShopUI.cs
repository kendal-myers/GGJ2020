using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    public GameObject currencyImage;
    public float rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currencyImage.transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
    }
}
