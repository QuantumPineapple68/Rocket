using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    private Vector3 startingVector;
    [SerializeField] private Vector3 momentVector;
    [SerializeField] [Range(0,1)] private float momentFactor;
    [SerializeField] private float period = 5f;
    void Start()
    {
        startingVector = transform.position;
    }
    
    void Update()
    {
        if (period <= Mathf.Epsilon) // mathf.epsilon is the smallest built in unity float
        {
            return;
        }
        float cycles = Time.time / period;
        const float tau = Mathf.PI * 2;
        float sinWave = Mathf.Sin(cycles * tau);
        momentFactor = (sinWave + 1f) / 2;
        
        Vector3 offset = momentFactor * momentVector;
        transform.position = startingVector + offset;
    }
}
