using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryPointController : MonoBehaviour
{
    DeliveryController deliveryController;

    // Start is called before the first frame update
    void Start()
    {
        deliveryController = GameObject.FindGameObjectWithTag("Player").GetComponent<DeliveryController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Delivered()
    {
        Debug.Log("NextPickup");
        deliveryController.NextPickup();
        gameObject.SetActive(false);
        
    }
}
