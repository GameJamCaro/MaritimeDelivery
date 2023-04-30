using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    Vector3 randomTargetPos;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(WaitAndChangeDir());
        agent.speed = Random.Range(10,15);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator WaitAndChangeDir()
    {
        ChangeDir();
        yield return new WaitForSeconds(3);
        StartCoroutine(WaitAndChangeDir());

    }

    void ChangeDir()
    {
        randomTargetPos = new Vector3(Random.Range(-500, 500), Random.Range(-500, 500), Random.Range(-500, 500));
        agent.SetDestination(randomTargetPos);
    }


    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.tag == "Enemy")
        //{
        //    ChangeDir();
        //}
    }


}
