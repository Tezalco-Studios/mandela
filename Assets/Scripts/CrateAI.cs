using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CrateAI : MonoBehaviour
{

    private NavMeshAgent agent;
    private GameObject destination;
    private GameObject endDestination;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        destination = GameObject.Find("NavDestination");
        endDestination = GameObject.Find("EndDestination");

        agent.SetDestination(destination.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetNewDestination() 
        => agent.SetDestination(endDestination.transform.position);
}
