using UnityEngine;
using UnityEngine.AI;


public class Bot : MonoBehaviour
{
    NavMeshAgent agent;
    public GameObject target;
    [SerializeField]int NearDistance = 1;
    [SerializeField]int FarDistance = 10;
    bool fleeing = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
    }


    void Seek(Vector3 location)
    {
        agent.SetDestination(location);
    }


    void Flee(Vector3 location)
    {
        Vector3 fleeVector = location - this.transform.position;
        agent.SetDestination(this.transform.position - fleeVector);
    }


    void Pursue()
    {
        Vector3 targetDir = target.transform.position - this.transform.position;


        float lookAhead = targetDir.magnitude/(agent.speed + target.GetComponent<Drive>().currentSpeed);
        Seek(target.transform.position + target.transform.forward * lookAhead);
    }


    // Update is called once per frame
    void Update()
    {
        UnityEngine.Vector3 distVec = target.transform.position - this.transform.position;
        float dist = distVec.magnitude;

        if(dist <= NearDistance && !fleeing)
        {
            fleeing = true;
        }

        if (dist >= FarDistance && fleeing)
        {
            fleeing = false;
        }


        if (fleeing)
        {
            Flee(target.transform.position);
        }
        else if(!fleeing)
        {
            Pursue();
        }
    }
}
