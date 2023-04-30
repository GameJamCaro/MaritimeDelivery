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
    public GameObject winPanel;
    int points;
    public TextMeshProUGUI pointsText;
    bool speedDecreasing;
    float horizontal;
    float vertical;
    Rigidbody rb;
    public Transform moveDir;
    CharacterController character;
    private Vector3 moveDirection;
    public float speed;
    private float yRotation;


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        agent = GetComponent<NavMeshAgent>();
        camDistance = Vector3.Distance(transform.position, cam.transform.position);
        deathPanel.SetActive(false);
        winPanel.SetActive(false);
        rb = GetComponent<Rigidbody>();
        character = GetComponent<CharacterController>();
        Cursor.visible = false;
    }

    void PlayerInput()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }


    void PlayerMoveCharacterController()
    {
        yRotation += horizontal;
        transform.rotation = Quaternion.Euler(0, yRotation, 0);

        moveDirection = moveDir.forward * vertical;


        character.Move(moveDirection * speed * Time.deltaTime);
           
    }




    
    // Update is called once per frame
    void Update()
    {
        playerPosition = transform.position;
        playerPosition.y = camDistance;
        playerPosition.z -= camOffset;
        cam.transform.position = playerPosition;

        PlayerInput();

        PlayerMoveCharacterController();


        //if (Input.GetMouseButtonDown(0))
        //{
        //    Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;

        //    if (Physics.Raycast(ray, out hit))
        //    {
        //        targetDestination.transform.position = hit.point;
        //        agent.SetDestination(hit.point);
        //    }


        //}
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 100;
            
        }
        else
        {
            speed = 50;
        }
        
    }


    //player controller 3d

    

    
    //Get mouse click position
    //void OnMouseDown()
    //{
    //    Debug.Log("Mouse Clicked");
    //    Vector3 mousePos = Input.mousePosition;
    //    mousePos.z = 10;
    //    Vector3 objectPos = Camera.main.ScreenToWorldPoint(mousePos);
    //    Debug.Log(objectPos);
    //    agent.SetDestination(objectPos);
    //}

    //Get mouse click position on ground

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Player Dead");
            Destroy(other.gameObject);
            deathPanel.SetActive(true);
            Cursor.visible = true;
            Time.timeScale = 0;
            
        }
        if (other.gameObject.tag == "Pickup")
        {
            Debug.Log("Pickup");
            other.GetComponent<PickupController>().PickedUp();
           
        }
        if (other.gameObject.tag == "DeliveryPoint")
        {
            
            if (other.gameObject.activeSelf  && !other.GetComponent<DeliveryPointController>().delivered)
            {
                Debug.Log("DeliveryPoint");
                other.GetComponent<DeliveryPointController>().Delivered();
                if (points < 5)
                {
                    points++;
                    pointsText.text = "Score: " + points + " / 5";
                }
                else
                {
                    winPanel.SetActive(true);
                    Cursor.visible = true;
                    Time.timeScale = 0;
                }
            }
           
           
        }
    }


    IEnumerator WaitAndDecreaseSpeed()
    {
        yield return new WaitForSeconds(5);
        agent.speed = 15;
    }
}
