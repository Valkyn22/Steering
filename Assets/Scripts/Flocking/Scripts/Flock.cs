using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockAgent agentPrefab;
    List<FlockAgent> agents = new List<FlockAgent>();
    public FlockBehavior behavior;

    [Range(5, 50)]
    public int startingCount = 15;
    const float agentDensity = 0.2f;

    [Range(1f, 100f)]
    public float driveFactor = 10f;

    [Range(1f, 100f)]
    public float maxSpeed = 5f;

    [Range(1f, 20)]
    public float neighborRadius = 2f;

    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;

    float sqMaxSpeed;
    float sqNeighborRadius;
    float sqAvoidanceRadius;
    public float SqAvoidanceRadius { get { return sqAvoidanceRadius; } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sqMaxSpeed = maxSpeed * maxSpeed;
        sqNeighborRadius = neighborRadius * neighborRadius;
        sqAvoidanceRadius = sqNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        for (int i = 0; i < startingCount; i++)
        {
            FlockAgent newAgent = Instantiate(
                agentPrefab,
                Random.insideUnitSphere * startingCount * agentDensity,
                Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)),
                transform
                );
            newAgent.name = "Agent" + i;
            agents.Add(newAgent);
        }
    }

    // Update is called once per frame
    void Update()
    {
       foreach (FlockAgent agent in agents)
        {
            List<Transform> context = GetNearbyObjects(agent);

            Vector3 move = behavior.CalculateMove(agent, context, this);
            move *= driveFactor;
            if(move.sqrMagnitude > sqMaxSpeed)
            {
                move = move.normalized * maxSpeed;
            }

            agent.Move(move);
        }
    }

    List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();
        Collider[] contextColliders = Physics.OverlapSphere(agent.transform.position, neighborRadius);
        foreach (Collider collider in contextColliders)
        {
            if (collider != agent.AgentCollider)
            {
                context.Add(collider.transform);
            }
        }
        return context;
    }
}
