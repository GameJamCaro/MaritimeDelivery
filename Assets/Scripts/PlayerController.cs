using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using UnityEngine.UI;

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
    
    float horizontal;
    float vertical;
   
    public Transform moveDir;
    CharacterController character;
    private Vector3 moveDirection;
    public float speed;
    private float yRotation;
    public int maxLives;
    int currentLives;
    public Image[] lives;
    GameObject[] enemies;
    public AudioSource audioSource;
    public AudioClip[] audioClips;
    Vector3 tempPos;


    // Start is called before the first frame update
    void Start()
    {
        
        agent = GetComponent<NavMeshAgent>();
        camDistance = Vector3.Distance(transform.position, cam.transform.position);
        deathPanel.SetActive(false);
        winPanel.SetActive(false);
       
        character = GetComponent<CharacterController>();
       
        currentLives = maxLives;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    void PlayerInput()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }


    void PlayerMoveCharacterController()
    {
        yRotation += horizontal * Time.deltaTime * 60;
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

       
        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 100;
            
        }
        else
        {
            speed = 50;
        }
        
    }


    
    bool once;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Whale"))
        {
            if (!once) {
                if (currentLives > 1)
                {
                    PlayAudio(audioClips[2]);
                    currentLives--;
                    Debug.Log("Player Hit");
                    lives[currentLives].enabled = false;
                    once = true;
                    StartCoroutine(WaitAndReset());
                }
                else
                {
                    PlayAudio(audioClips[2]);
                    currentLives--;
                    lives[currentLives].enabled = false;
                    Debug.Log("Player Dead");
                   
                    deathPanel.SetActive(true);
                    Cursor.visible = true;
                    Time.timeScale = 0;
                }
            }
           
            
        }
        if (other.gameObject.tag == "Pickup")
        {
            Debug.Log("Pickup");
            PlayAudio(audioClips[1]);
            other.GetComponent<PickupController>().PickedUp();
            foreach (GameObject enemy in enemies)
            {
                var enemyController = enemy.GetComponent<EnemyController>();
                enemyController.SetPlayerAsTarget();
            }

            if (points == 1)
            {
                foreach (GameObject enemy in enemies)
                {
                    var enemyController = enemy.GetComponent<EnemyController>();
                   
                    enemyController.agent.speed = 15;
                    enemyController.GetComponent<EnemyController>().changeDirTime = 5;
                }
            }
            else if (points == 2)
            {
                foreach (GameObject enemy in enemies)
                {
                    var enemyController = enemy.GetComponent<EnemyController>();
                   
                    enemyController.agent.speed = 25;
                    enemyController.changeDirTime = 4;
                }
            }
            else if (points == 3)
            {
                foreach (GameObject enemy in enemies)
                {
                    var enemyController = enemy.GetComponent<EnemyController>();
                    
                    enemyController.agent.speed = 35;
                    enemyController.changeDirTime = 3;
                }
            }
            else if (points == 4)
            {
                foreach (GameObject enemy in enemies)
                {

                    var enemyController = enemy.GetComponent<EnemyController>();
                   
                    enemyController.agent.speed = 45;
                    enemyController.changeDirTime = 2;
                }
            }

        }
        if (other.gameObject.tag == "DeliveryPoint")
        {
            
            if (other.gameObject.activeSelf  && !other.GetComponent<DeliveryPointController>().delivered)
            {
                if (points < 5)
                {
                    if (points < 4)
                    {
                        other.GetComponent<DeliveryPointController>().Delivered();
                        PlayAudio(audioClips[0]);
                        foreach (GameObject enemy in enemies)
                        {
                            var enemyController = enemy.GetComponent<EnemyController>();
                            enemyController.ChangeDir();
                        }
                    }
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


    void PlayAudio(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    IEnumerator WaitAndDecreaseSpeed()
    {
        yield return new WaitForSeconds(5);
        agent.speed = 15;
    }


    IEnumerator WaitAndReset()
    {
        yield return new WaitForSeconds(2);
        once = false;
    }
}
