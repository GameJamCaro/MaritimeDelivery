using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    Camera cam;
    public Camera mapCam;
    bool mapOn;



    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        cam.fieldOfView = 80;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.M))
        {
           
                mapCam.gameObject.SetActive(true);
                cam.enabled = false;
                mapOn = true;
                
           
           
        }
        else
        {
            cam.enabled = true;
            mapCam.gameObject.SetActive(false);
        }


    }
}
