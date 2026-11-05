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

    [Header("Leaders")]
    [Range(0, 10)] public int leaderCount = 1;          // the leader selector
    public FlockBehavior leaderBehavior;                 // wander + obstacle avoidance
    [Range(0.1f, 1f)] public float leaderSpeedMultiplier = 0.6f; // slower so followers catch up
    public Color leaderColor = Color.red;
    public Color followerColor = Color.white;
    public float wanderRadius = 20f;                     // area the leaders roam in

    public List<FlockAgent> Leaders { get; private set; } = new List<FlockAgent>();
    int appliedLeaderCount = -1;

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
        // Re-assign leaders live if the slider changed (also works in Play mode)
        if (leaderCount != appliedLeaderCount) ApplyLeaderCount();

        foreach (FlockAgent agent in agents)
        {
            List<Transform> context = GetNearbyObjects(agent);

            FlockBehavior b = (agent.IsLeader && leaderBehavior != null) ? leaderBehavior : behavior;
            float speedLimit = agent.IsLeader ? maxSpeed * leaderSpeedMultiplier : maxSpeed;

            Vector3 move = b.CalculateMove(agent, context, this) * driveFactor;
            if (move.sqrMagnitude > speedLimit * speedLimit)
                move = move.normalized * speedLimit;

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

    void ApplyLeaderCount()
    {
        int count = Mathf.Clamp(leaderCount, 0, agents.Count);
        Leaders.Clear();
        for (int i = 0; i < agents.Count; i++)
        {
            bool isLeader = i < count;
            agents[i].SetLeader(isLeader, isLeader ? leaderColor : followerColor);
            if (isLeader) Leaders.Add(agents[i]);
        }
        appliedLeaderCount = leaderCount;
    }
}
