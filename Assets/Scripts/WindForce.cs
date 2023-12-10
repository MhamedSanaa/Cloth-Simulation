using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindForce : MonoBehaviour
{
    //public float amplitude = 0.5f;  // Adjust the amplitude of the sinusoidal wind
    //public float frequency = 2.0f;  // Adjust the frequency of the sinusoidal wind
    public Vector3 windVelocity;
    public float windStrength;
    public float windRadius = 5.0f;
    public Vector3 windForce;

    // Start is called before the first frame update
    void Start()
    {
        windVelocity = new Vector3(Random.Range(-1.0f, 1.0f), 0.0f, Random.Range(-1.0f, 1.0f)).normalized;
        windStrength = Random.Range(0.2f, 10.0f);
        windForce = windVelocity * windStrength;
    }

    // Update is called once per frame
    void Update()
    {
        windVelocity = new Vector3(Random.Range(-1.0f, 1.0f), 0.0f, Random.Range(-1.0f, 1.0f)).normalized;
        windStrength = Random.Range(0.2f, 10.0f);
        windForce = windVelocity * windStrength;
    }
}
