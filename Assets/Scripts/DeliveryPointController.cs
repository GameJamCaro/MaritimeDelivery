using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryPointController : MonoBehaviour
{
    DeliveryController deliveryController;
    GameObject cup;
    GameObject person;
    public bool delivered;

    // Start is called before the first frame update
    void Start()
    {
        deliveryController = GameObject.FindGameObjectWithTag("Player").GetComponent<DeliveryController>();
        cup = gameObject.transform.GetChild(2).gameObject;
        person = gameObject.transform.GetChild(0).gameObject;
        cup.SetActive(false);
    }

    

    public void Delivered()
    {
        if (!delivered)
        {
            Debug.Log("NextPickup");
            deliveryController.NextPickup();
            transform.GetChild(1).gameObject.SetActive(false);
            cup.SetActive(true);
            person.SetActive(false);
            delivered = true;
        }
    }
}
