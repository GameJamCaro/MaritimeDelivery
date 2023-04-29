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
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator WaitAndChangeDir()
    {
        randomTargetPos = new Vector3(Random.Range(-50, 50), Random.Range(-50, 50), Random.Range(-50, 50));
        agent.SetDestination(randomTargetPos);
        yield return new WaitForSeconds(5);
        StartCoroutine(WaitAndChangeDir());

    }

    
}
