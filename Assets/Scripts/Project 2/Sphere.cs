using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    public float mass = 1f;
    public PhysicsConfig config;
    Vector3 ForcesSum;
    Vector3 acc;
    Vector3 vel;
    public List<string> affectedParticles = new List<string>();

    public float sphereRadius;


    // Start is called before the first frame update
    void Start()
    {
        sphereRadius = transform.localScale.y / 2;
        Debug.Log("sphere radius = " + sphereRadius);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        ForcesSum = (new Vector3(0f, -config.g, 0f)) * mass; 
        acc += ForcesSum / mass;
        vel += acc * Time.deltaTime;
        transform.position += vel * Time.deltaTime;
        */
    }

    public List<string> getAffectedParticles(){
        return affectedParticles;
    }

    public void addAffectedParticle(string nameAP){
        affectedParticles.Add(nameAP);
    }

    public void removeAffectedParticle(string nameAP){
        affectedParticles.Remove(nameAP);
    }

}
