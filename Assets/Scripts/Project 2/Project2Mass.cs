using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Project2Mass : MonoBehaviour
{
    
    public PhysicsConfig config;
    public float mass ;
    public float dragCoefficient ;
    Vector3 dragForce;

    public bool isStationary = false;

    Vector3 acceleration;
    public Vector3 velocity = new Vector3(0,0,0);

    public IDictionary<string, Vector3> AllSpringForces = new Dictionary<string, Vector3>();
    Vector3 ForcesSum;
    public GameObject sphereObject;

    float distance = 0f;
    Vector3 pushForce = new Vector3(0,0,0);

    void Start(){
        //Hashtable AllForces = new Hashtable();
        //AllForces = new Dictionary<string, Vector3>();
        sphereObject = GameObject.Find("Sphere");

    }

    public float getMass(){
        return mass;
    }

    public Vector3 getVelocity(){
        return velocity;
    }


    public void setAcceleration(Vector3 acc){
        acceleration = acc;
    }

    public void setStationary(bool val){
        isStationary = val;
    }

    public void registerSpringForce(string key){
        Vector3 init = new Vector3(0f,0f,0f);
        AllSpringForces.Add(new KeyValuePair<string, Vector3>(key,init));
    }

    public void updateSpringForce(string key,Vector3 value){
        AllSpringForces[key]= value;
    }

    void Update(){
        
        mass = config.vertexMass;
        dragCoefficient = config.vertexDragCoefficient;
        
        if(!isStationary){
            
            CheckCollision();
            //Debug.Log("Particle Count: " + sphereObject.GetComponent<Sphere>().getAffectedParticles().Count);
            foreach (string particle in sphereObject.GetComponent<Sphere>().getAffectedParticles())
            {
                //Debug.Log(particle);
            }

            ForcesSum = new Vector3 (0f,0f,0f);
            foreach(KeyValuePair<string, Vector3> force in AllSpringForces){
                ForcesSum += force.Value;
            }
            ForcesSum += - dragCoefficient * velocity;  // drag force
            ForcesSum += pushForce;
            acceleration =  ForcesSum / mass;
            velocity += acceleration * Time.deltaTime;
            transform.position += velocity * Time.deltaTime;
        }

    }
    public bool Exist(string name)
    {
        foreach(string particle in sphereObject.GetComponent<Sphere>().getAffectedParticles())
        {
            if (particle.Equals(name))
            {
                return true;
            }
        }
        return false;
    }

    public void CheckCollision()
    {
        distance = sphereObject.transform.position.y -  transform.position.y;
        if (distance < sphereObject.GetComponent<Sphere>().sphereRadius)
        {
            

            float d = Vector3.Distance(sphereObject.transform.position, transform.position);
            if(d< sphereObject.GetComponent<Sphere>().sphereRadius)
            {
                if (!Exist(this.name))
                {
                    sphereObject.GetComponent<Sphere>().addAffectedParticle(this.name); 
                    pushForce = 20f * (transform.position-sphereObject.transform.position).normalized;
                }
                
            }else{ 
                sphereObject.GetComponent<Sphere>().removeAffectedParticle(this.name);
                pushForce = new Vector3(0,0,0);  
                
               

            }
        }

    }
}
