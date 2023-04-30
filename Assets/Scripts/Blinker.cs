using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinker : MonoBehaviour
{
    Light light;
    
    
    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
        StartCoroutine(WaitAndBlink());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator WaitAndBlink()
    {
        light.enabled = true;
        yield return new WaitForSeconds(.5f);
        light.enabled = false;
        yield return new WaitForSeconds(.5f);
        StartCoroutine(WaitAndBlink());
    }
}
