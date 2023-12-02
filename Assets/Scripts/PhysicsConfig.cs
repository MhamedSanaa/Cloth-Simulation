using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "PhysicsConfig", order = 1)]
public class PhysicsConfig : ScriptableObject
{
    public float g = 9.81f;
    
}
