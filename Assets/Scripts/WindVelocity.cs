using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindVelocity : MonoBehaviour
{
    public Vector3 windVector;
    public float windStrength = 10f;
    public float windRadius = 5.0f;
    public Vector3 windVelocity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        windVector = new Vector3(Random.Range(0f, 1.0f), 0.0f, 0f).normalized;
        //windStrength = 10f;
        windVelocity = windVector * windStrength;
    }
}
