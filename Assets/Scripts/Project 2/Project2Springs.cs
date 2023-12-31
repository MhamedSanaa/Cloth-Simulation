using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Project2Springs : MonoBehaviour
{
    public PhysicsConfig config;
    float relaxedLength = 3f;
    public float springCoefficient = 6f;
    
    public float dampingCoefficient;
    public GameObject object1;
    public GameObject object2;

    Vector3 displacement;
    Vector3 springForce;
    Vector3 dampingForce;

    LineRenderer lr;

    // Start is called before the first frame update
    void Start()
    {
        //object1.GetComponent<Project2Mass>().registerSpringForce(this.name);
        //object2.GetComponent<Project2Mass>().registerSpringForce(this.name);
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        dampingCoefficient = config.springDampingCoefficient;

        if(object1 != null && object2 !=null){
            //Debug.DrawLine(object1.transform.position, object2.transform.position, Color.yellow);
            lr.SetPosition(0,object1.transform.position);
            lr.SetPosition(1,object2.transform.position);
            displacement = object1.transform.position - object2.transform.position;
            springForce  = -springCoefficient * (displacement.magnitude - relaxedLength) * displacement.normalized;
            dampingForce = -(dampingCoefficient * displacement.normalized*(Vector3.Dot(object1.GetComponent<Project2Mass>().getVelocity()-object2.GetComponent<Project2Mass>().getVelocity(),displacement))/displacement.magnitude);
            
            object1.GetComponent<Project2Mass>().updateSpringForce(this.name,( springForce + dampingForce ));
            object2.GetComponent<Project2Mass>().updateSpringForce(this.name,-( springForce + dampingForce ));
            }
    }

    
    public void setRelaxed( float rlen){
        relaxedLength = rlen ;
    }
    public void setObject1( GameObject v){
        object1 = v ;
        object1.GetComponent<Project2Mass>().registerSpringForce(this.name);
    }
    public void setObject2( GameObject v){
        object2 = v ;
        object2.GetComponent<Project2Mass>().registerSpringForce(this.name);
    }
    
}
