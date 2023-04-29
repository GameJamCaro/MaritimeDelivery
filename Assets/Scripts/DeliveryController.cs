using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryController : MonoBehaviour
{
    public GameObject[] pickups;
    public GameObject[] deliveryPoints;
    int randomPickupID;
    int currentPickpID;
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
        currentPickpID = Random.Range(0, pickups.Length);
        pickups[currentPickpID].SetActive(true);
        oldPickupIds.Add(currentPickpID);

    }

    public void NextPickup()
    {
        randomPickupID = Random.Range(0, pickups.Length);
        if (oldPickupIds.Contains(randomPickupID))
        {
            NextPickup();
        }
        else
        {
            currentPickpID = Random.Range(0, pickups.Length);
            pickups[currentPickpID].SetActive(true);
            oldPickupIds.Add(currentPickpID);
        }
    }
}
