using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringForces : MonoBehaviour
{
    public float relaxedLength = 3f;
    public float springConstant = 6f;
    public float dampingCoefficient = 0.4f;
    public Transform objectTransform1 = null;
    public Transform objectTransform2 = null;

    public Vector3 displacement = Vector3.zero;
    public Vector3 velocity = Vector3.zero;

    public Vector3 objectVelocity1 = Vector3.zero;
    public Vector3 objectVelocity2 = Vector3.zero;

    public Vector3 objectPosition1 = Vector3.zero;
    public Vector3 objectPosition2 = Vector3.zero;

    public Vector3 springForce = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        objectPosition1 = objectTransform1.position;
        objectPosition2 = objectTransform2.position;
        displacement = objectPosition2 - objectPosition1;
    }

    // Update is called once per frame
    void Update()
    {
        objectVelocity1 = (objectTransform1.position-objectPosition1)/Time.deltaTime;
        objectPosition1 = objectTransform1.position;

        objectVelocity2 = (objectTransform2.position - objectPosition2) / Time.deltaTime;
        objectPosition2 = objectTransform2.position;

        velocity = objectVelocity2 - objectVelocity1;

        springForce = -(displacement.normalized * springConstant * (displacement.magnitude - relaxedLength)) - (dampingCoefficient * displacement.normalized * Vector3.Dot(velocity, displacement) / displacement.magnitude);
        displacement = objectPosition2 - objectPosition1;
    }
}
