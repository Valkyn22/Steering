using UnityEngine;
using UnityEngine.AI;


public class Bot : MonoBehaviour
{
    NavMeshAgent agent;
    public GameObject target;


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
        //Seek(target.transform.position);
        Pursue();
    }
}
