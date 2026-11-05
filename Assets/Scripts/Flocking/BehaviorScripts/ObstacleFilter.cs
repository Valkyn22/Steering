using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Flock/Behavior/Obstacle Filter")]

public class ObstacleFilter : ScriptableObject
{
    public LayerMask mask;

    public List<Transform> Filter(FlockAgent agent, List<Transform> original)
    {
        List<Transform> filtered = new List<Transform>();
        foreach (Transform item in original)
        {
            if(mask == (mask | (1 << item.gameObject.layer)))
            {
                filtered.Add(item);
            }
        }

        return filtered;
    }
}
