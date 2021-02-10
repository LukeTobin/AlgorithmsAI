using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// BASE CLASS FOR STEERING BEHAVIORS
public abstract class SteeringBehavior : MonoBehaviour
{
    [Header("Displacement")]
    public Vector2 acceleration;
    public Vector2 velocity;

    [Header("Vechile Driven Stats")]
    public float maxSpeed;
    public float maxForce;
    public float mass;

    [Header("Refs")]
    public Vechile vechile;

    void Start() {
        // reset our acceleration and velocity. This makes sure no external forces are being applied when the Vechile is instansiated
        acceleration = Vector2.zero;
        velocity = Vector2.zero;
    }

    public virtual void Steering(Vector2 targetPostion){
        // Get our current distance from the target compared to our current position
        Vector2 distance = (targetPostion - (Vector2)transform.position);
        // Get the acceleration towards our target, based on distance
        acceleration = distance.normalized * maxSpeed;
        // Setup our steering vector based on our current acceleration and velocity
        Vector2 steering = acceleration - velocity;
        // apply our steering value to our current velocity each frame
        velocity += steering * Time.deltaTime;
        // update our vechiles position based on our velocity each frame
        transform.position += (Vector3)velocity * Time.deltaTime;
    }
}
