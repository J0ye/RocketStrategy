using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Rocket : MonoBehaviour
{
    public Vector3 target; // Target position for the rocket
    public AnimationCurve heightCurve;
    public float heightValueMultiplier;
    public float duration = 20f; // Duration to reach the target
    private float elapsedTime = 0f; // Track elapsed time
    private Vector3 startPosition = Vector3.zero;

    void Awake()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration; // Set t to be time as value between 0 and 1

            // Calculate the Bézier curve position
            Vector3 flightPath = CalculateFlightPath(startPosition, target, t);

            Vector3 direction = (transform.position -flightPath).normalized;
            if (direction != Vector3.zero) // Check to avoid NaN
            {
                Quaternion lookRotation = Quaternion.LookRotation(-direction);
                transform.rotation = lookRotation;
            }
            transform.position = flightPath; // Move the rocket
        }
        else
        {
            Destroy(gameObject); // Destroy rocket after reaching the target
        }
    }

    Vector3 CalculateFlightPath(Vector3 start, Vector3 end, float t)
    {
        float curveValue = heightCurve.Evaluate(t);
        Vector3 ret = Vector3.Lerp(start, end, t); 
        ret = new Vector3(ret.x, curveValue*heightValueMultiplier, ret.z);
        return ret;
    }
}
