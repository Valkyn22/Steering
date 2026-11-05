using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class FlockAgent : MonoBehaviour
{
    CapsuleCollider agentCollider;
    public CapsuleCollider AgentCollider {  get { return agentCollider; } }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agentCollider = GetComponent<CapsuleCollider>();
    }


    public void Move(Vector3 velocity)
    {
        transform.forward = velocity;
        transform.position += velocity * Time.deltaTime;
    }
}
