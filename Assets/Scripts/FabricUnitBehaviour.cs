using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FabricUnitBehaviour : MonoBehaviour
{
    public float mass =1f;
    public float g=9.81f;
    public float dragCoefficent = 0.4f;
    public List<GameObject> springs = new List<GameObject>();
    public List<Vector3> springForces = new List<Vector3>();

    /*public GameObject spring1;
    public GameObject spring2;

    public Vector3 springForce1;
    public Vector3 springForce2;*/


    public Vector3 gravity = Vector3.zero;
    public Vector3 gravitationlAcceleration = Vector3.zero;
    public Vector3 velocity = Vector3.zero;
    public Vector3 acceleration = Vector3.zero;
    public Vector3 dragForce = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var spring in springs)
        {
            springForces.Add(spring.GetComponent<SpringForces>().springForce);
        }
        gravity = new Vector3(0, -g, 0);
    }

    // Update is called once per frame
    void Update()
    {
        /*springForce1 = spring1.GetComponent<SpringForces>().springForce;
        springForce2 = spring2.GetComponent<SpringForces>().springForce;
        Debug.Log(springForce1);*/
        springForces.Clear();

        foreach (var spring in springs)
        {
            springForces.Add(spring.GetComponent<SpringForces>().objectTransform2.name != transform.name ? -spring.GetComponent<SpringForces>().springForce : spring.GetComponent<SpringForces>().springForce);
        }
        gravitationlAcceleration = gravity * mass;
        Vector3 forces = (gravitationlAcceleration + dragForce);

        if (springForces.Count > 0)
        {
            foreach (var springForce in springForces)
            {
                forces += springForce;
            }
        }
        acceleration = forces / mass;
        velocity +=  acceleration * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;
        dragForce = -dragCoefficent * velocity;
    }
}
