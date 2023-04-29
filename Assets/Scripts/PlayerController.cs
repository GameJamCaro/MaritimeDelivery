using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    NavMeshAgent agent;
    
    public Camera cam;
    public GameObject targetDestination;
    Vector3 playerPosition;
    float camDistance;
    public float camOffset;
    public GameObject deathPanel;
    int points;
    public TextMeshProUGUI pointsText;
    bool speedDecreasing;


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        agent = GetComponent<NavMeshAgent>();
        camDistance = Vector3.Distance(transform.position, cam.transform.position);
        deathPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = transform.position;
        playerPosition.y = camDistance;
        playerPosition.z -= camOffset;
        cam.transform.position = playerPosition;


        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                targetDestination.transform.position = hit.point;
                agent.SetDestination(hit.point);
            }


        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            agent.speed = 25;
            
        }
        
    }

    //Get mouse click position
    void OnMouseDown()
    {
        Debug.Log("Mouse Clicked");
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10;
        Vector3 objectPos = Camera.main.ScreenToWorldPoint(mousePos);
        Debug.Log(objectPos);
        agent.SetDestination(objectPos);
    }

    //Get mouse click position on ground

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Player Dead");
            Destroy(other.gameObject);
            deathPanel.SetActive(true);
            Time.timeScale = 0;
        }
        if (other.gameObject.tag == "Pickup")
        {
            Debug.Log("Pickup");
            other.GetComponent<PickupController>().PickedUp();
           
        }
        if (other.gameObject.tag == "DeliveryPoint")
        {
            Debug.Log("Delivered");
            if (other.gameObject.activeSelf)
            {
                other.GetComponent<DeliveryPointController>().Delivered();
            }
            points++;
            pointsText.text = "Score: " + points + " / 5";
        }
    }


    IEnumerator WaitAndDecreaseSpeed()
    {
        yield return new WaitForSeconds(5);
        agent.speed = 15;
    }
}
