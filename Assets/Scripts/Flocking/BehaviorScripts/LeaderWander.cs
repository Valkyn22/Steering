using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Leader Wander")]
public class LeaderWander : FlockBehavior
{
    public float arriveDistance = 1.5f;

    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        if (!agent.HasWanderTarget ||
            (agent.WanderTarget - agent.transform.position).sqrMagnitude < arriveDistance * arriveDistance)
        {
            Vector3 p = Random.insideUnitSphere * flock.wanderRadius;
            agent.WanderTarget = flock.transform.position + p;
            agent.HasWanderTarget = true;
        }
        return (agent.WanderTarget - agent.transform.position).normalized;
    }
}