using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    public bool isPickedUp = false;
    public GameObject deliveryPoint;
   

    

    public void PickedUp()
    {
        deliveryPoint.SetActive(true);
        Destroy(gameObject);    
        //transform.GetChild(2).gameObject.SetActive(false);
    }
    
        
}
