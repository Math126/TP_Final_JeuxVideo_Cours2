using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Luciole_Group_PNJ : MonoBehaviour
{
    NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        InvokeRepeating("SetRandomDestination", 0f, 7f);
    }

    void SetRandomDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * 50f;
        randomDirection += transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, 50f, NavMesh.AllAreas))
        {
            Vector3 destination = new Vector3(hit.position.x, 0.75f, hit.position.z);
            agent.SetDestination(destination);
        }
    }
}
