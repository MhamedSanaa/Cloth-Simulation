using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindVelocity : MonoBehaviour
{
    public Vector3 windVector;
    public float windStrength = 10f;
    public float windRadius = 5.0f;
    public Vector3 windVelocity;
    bool activated = false;

    // Start is called before the first frame update
    void Start()
    {
        windStrength =0f;
    }

    // Update is called once per frame
    void Update()
    {
        windVector = new Vector3(Random.Range(-1.0f, 1.0f), 0.0f, 0f);
        //windStrength = 10f;
        windVelocity = windVector * windStrength;
    }
    public void activateWind(){
    if (activated){
        windStrength = 0f;
        activated = false;
    }
    else {
        windStrength = 400f;
        activated = true;
    }

    }
}
