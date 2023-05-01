using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryController : MonoBehaviour
{
    public GameObject[] pickups;
    public GameObject[] deliveryPoints;
    int randomPickupID;
    int currentPickupID;
    List<int> oldPickupIds;
    

    void Start()
    {
        oldPickupIds = new List<int>();
        foreach (var pickup in pickups)
        {
            pickup.SetActive(false);
        }
        foreach (var deliveryPoint in deliveryPoints)
        {
            deliveryPoint.SetActive(false);
        }
        NextPickup();

    }

    public void NextPickup()
    {
        if (pickups.Length > 0) 
        {
            currentPickupID = Random.Range(0, pickups.Length);
        }
        if (oldPickupIds.Contains(currentPickupID))
        {
            NextPickup();
        }
        else
        {
            if (pickups[currentPickupID] != null)
            {
                pickups[currentPickupID].SetActive(true);
                oldPickupIds.Add(currentPickupID);
            }
        }
    }
}
