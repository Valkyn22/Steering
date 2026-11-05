using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class FlockAgent : MonoBehaviour
{
    CapsuleCollider agentCollider;
    public CapsuleCollider AgentCollider {  get { return agentCollider; } }


    public bool IsLeader { get; private set; }
    public Vector3 WanderTarget { get; set; }   // Used by the leader's wander behavior
    public bool HasWanderTarget { get; set; }

    Renderer rend;

    // Awake instead of Start so the collider is ready before Flock uses it
    void Awake()
    {
        agentCollider = GetComponent<CapsuleCollider>();
        rend = GetComponentInChildren<Renderer>();
    }

    public void SetLeader(bool isLeader, Color color)
    {
        IsLeader = isLeader;
        HasWanderTarget = false;
        if (rend != null) rend.material.color = color;
    }

    public void Move(Vector3 velocity)
    {
        if (velocity.sqrMagnitude > 0.0001f)
            transform.forward = velocity;
        transform.position += velocity * Time.deltaTime;
    }
}
