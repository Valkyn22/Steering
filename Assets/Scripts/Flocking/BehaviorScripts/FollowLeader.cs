using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Follow Leader")]
public class FollowLeader : FlockBehavior
{
    public float stopDistance = 2f;
    public float slowRadius = 6f;

    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        // Follow the closest leader (works with several leaders)
        FlockAgent nearest = null;
        float best = float.MaxValue;
        foreach (FlockAgent leader in flock.Leaders)
        {
            if (leader == agent) continue;
            float sq = (leader.transform.position - agent.transform.position).sqrMagnitude;
            if (sq < best) { best = sq; nearest = leader; }
        }
        if (nearest == null) return Vector3.zero;

        Vector3 offset = nearest.transform.position - agent.transform.position;
        float dist = offset.magnitude;
        if (dist < stopDistance) return Vector3.zero;

        return offset.normalized * Mathf.Clamp01(dist / slowRadius);
    }
}