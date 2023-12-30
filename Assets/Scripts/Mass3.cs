using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mass3 : MonoBehaviour
{
    public float mass = 1f;
    public PhysicsConfig config;
    public float dragCoefficient = 0.4f;
    Vector3 dragForce;

    public bool isStationary = false;

    Vector3 acceleration;
    public Vector3 velocity = new Vector3(0, 0, 0);

    public IDictionary<string, Vector3> AllSpringForces = new Dictionary<string, Vector3>();
    Vector3 ForcesSum;
    public GameObject windZone;

    public int indexi;
    public int indexj;


    void Start()
    {
        //Hashtable AllForces = new Hashtable();
        //AllForces = new Dictionary<string, Vector3>();
        windZone = GameObject.FindGameObjectWithTag("WindZone");
    }

    public float getMass()
    {
        return mass;
    }

    public Vector3 getVelocity()
    {
        return velocity;
    }


    public void setAcceleration(Vector3 acc)
    {
        acceleration = acc;
    }

    public void setStationary(bool val)
    {
        isStationary = val;
    }

    public void registerSpringForce(string key)
    {
        Vector3 init = new Vector3(0f, 0f, 0f);
        AllSpringForces.Add(new KeyValuePair<string, Vector3>(key, init));
    }

    public void updateSpringForce(string key, Vector3 value)
    {
        AllSpringForces[key] = value;
    }

    void Update()
    {
        GameObject[,] matrix = GameObject.Find("MeshCreator").GetComponent<MeshCreator>().mesh;
        Vector3 normal = Vector3.zero;
        if (!isStationary)
        {
            ForcesSum = (new Vector3(0f, -config.g, 0f)) * mass;
            float distance = MathF.Sqrt(MathF.Pow(transform.position.y - windZone.transform.position.y, 2) + MathF.Pow(transform.position.z - windZone.transform.position.z, 2));
            if (distance <= windZone.GetComponent<WindVelocity>().windRadius)
            {
                float distanceFactor = 1.0f - (distance / windZone.GetComponent<WindVelocity>().windRadius);

                if (indexj + 1 <= 20 && indexi + 1 < 20 && indexj - 1 >= 0 && indexi - 1 >= 0)
                {
                    Vector3 v1 = transform.position - matrix[indexi, indexj + 1].transform.position;
                    Vector3 v2 = transform.position - matrix[indexi + 1, indexj].transform.position;
                    Vector3 v11 = transform.position - matrix[indexi, indexj - 1].transform.position;
                    Vector3 v22 = transform.position - matrix[indexi - 1, indexj].transform.position;

                    Vector3 n = Vector3.Cross(v1, v2);
                    Vector3 n2 = Vector3.Cross(v11, v22);

                    normal = ((n + n2) / 2).normalized;
                }

                ForcesSum += Vector3.Scale(normal, Vector3.Scale(((windZone.GetComponent<WindVelocity>().windVelocity * distanceFactor) - velocity), normal))*0.5f;
            }
            foreach (KeyValuePair<string, Vector3> force in AllSpringForces)
            {
                ForcesSum += force.Value;
            }
            ForcesSum += -dragCoefficient * velocity;  // drag force
            acceleration = ForcesSum / mass;
            velocity += acceleration * Time.deltaTime;
            transform.position += velocity * Time.deltaTime;
        }

    }


}
