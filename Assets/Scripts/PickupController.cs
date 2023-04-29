using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    public bool isPickedUp = false;
    public GameObject deliveryPoint;
   

    // Update is called once per frame
    void Update()
    {

    }

    public void PickedUp()
    {
        deliveryPoint.SetActive(true);
        gameObject.SetActive(false);
    }
    
        
}
