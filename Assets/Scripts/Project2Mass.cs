using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Project2Mass : MonoBehaviour
{
    
    public PhysicsConfig config;
    float g = 9.81f;
    public float mass ;
    public float dragCoefficient ;
    Vector3 dragForce;

    public bool isStationary = false;

    Vector3 acceleration;
    public Vector3 velocity = new Vector3(0,0,0);

    public IDictionary<string, Vector3> AllSpringForces = new Dictionary<string, Vector3>();
    Vector3 ForcesSum;
    

    void Start(){
        //Hashtable AllForces = new Hashtable();
        //AllForces = new Dictionary<string, Vector3>();

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
            ForcesSum = (new Vector3(0f, -g, 0f)) * mass;
            
            foreach(KeyValuePair<string, Vector3> force in AllSpringForces){
                ForcesSum += force.Value;
            }
            ForcesSum += - dragCoefficient * velocity;  // drag force
            acceleration =  ForcesSum / mass;
            velocity += acceleration * Time.deltaTime;
            transform.position += velocity * Time.deltaTime;
        }

    }

    
}
