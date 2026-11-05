using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine.AI;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Flee", story: "[Self] flees from [Target]", category: "Action", id: "000e62a67e26aa829f1287653d5d2b60")]
public partial class FleeAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<float> FarDistance;
    [SerializeReference] public BlackboardVariable<float> CloseDistance;

    NavMeshAgent agent;

    protected override Status OnStart()
    {
        agent = Self.Value.GetComponent<NavMeshAgent>();
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        Vector3 selfVec = Self.Value.transform.position;
        Vector3 targetVec = Target.Value.transform.position;

        if (Vector3.Distance(selfVec, targetVec) >= FarDistance.Value)
        {
            return Status.Success;
        }
        agent.SetDestination(selfVec - (targetVec - selfVec));
        return Status.Running;
    }
}

