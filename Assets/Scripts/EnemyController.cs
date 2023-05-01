using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    Vector3 randomTargetPos;
    public NavMeshAgent agent;
    public int changeDirTime = 5;
    GameObject player;
    bool chasePlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(WaitAndChangeDir());
        agent.speed = Random.Range(10,15);
    }

    // Update is called once per frame
    void Update()
    {
        if (chasePlayer)
        {
            agent.SetDestination(player.transform.position);
        }
    }


    IEnumerator WaitAndChangeDir()
    {
        randomTargetPos = new Vector3(Random.Range(-500, 500), Random.Range(-500, 500), Random.Range(-500, 500));
        agent.SetDestination(randomTargetPos);        
        yield return new WaitForSeconds(3);
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
        //if (other.gameObject.tag == "Enemy")
        //{
        //    ChangeDir();
        //}
    }

    public void SetPlayerAsTarget()
    {
        StopAllCoroutines();
        chasePlayer = true;
        
    }


}
