using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "PhysicsConfig", order = 1)]
public class PhysicsConfig : ScriptableObject
{
    // Vertices config
    public float vertexMass =1f;
    public float vertexDragCoefficient = 0.4f;

    // Springs config
    public float springDampingCoefficient = 0.4f;
    
}
