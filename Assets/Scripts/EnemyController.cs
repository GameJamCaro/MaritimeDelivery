using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    Vector3 randomTargetPos;
    public NavMeshAgent agent;
    public int changeDirTime = 3;
    GameObject player;
    public bool chasePlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        agent.speed = Random.Range(10, 15);
        randomTargetPos = new Vector3(Random.Range(-500, 500), Random.Range(-500, 500), Random.Range(-500, 500));
        agent.SetDestination(randomTargetPos);
    
        StartCoroutine(WaitAndChangeDir());
       
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (chasePlayer)
        {
            agent.SetDestination(player.transform.position);
        }
        else
        {
            agent.SetDestination(randomTargetPos);
        }
    }


    IEnumerator WaitAndChangeDir()
    {
        yield return new WaitForSeconds(changeDirTime);
        ChangeDir();
    }

    public void ChangeDir()
    {
        chasePlayer = false;
        randomTargetPos = new Vector3(Random.Range(-500, 500), Random.Range(-500, 500), Random.Range(-500, 500));
        agent.SetDestination(randomTargetPos);
        StartCoroutine(WaitAndChangeDir());
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            ChangeDir();
        }
    }

    public void SetPlayerAsTarget()
    {
        //agent.SetDestination(player.transform.position);
        chasePlayer = true;
        Debug.Log("Enemy goes to Player");
    }


}
