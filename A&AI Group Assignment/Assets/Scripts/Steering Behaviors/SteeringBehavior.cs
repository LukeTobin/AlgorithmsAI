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
    public float radiusSize;

    [HideInInspector] public Vechile vechile;

    void Start() {
        // reset our acceleration and velocity. This makes sure no external forces are being applied when the Vechile is instansiated
        acceleration = Vector2.zero;
        velocity = Vector2.zero;
    }

    protected virtual void Steering(Vector2 targetPostion){
        // Get our current distance from the target compared to our current position
        Vector2 distance = (targetPostion - (Vector2)transform.position); //transform.position is a vector3, so we cast it to a Vector2 since we only need two axises
        // Get the acceleration towards our target, based on distance
        Vector2 desiredVelocity = distance.normalized * maxSpeed;
        // Setup our steering vector based on our current acceleration and velocity
        Vector2 steering = desiredVelocity - velocity;
        // Truncate our steering vector based around our maxSpeed
        acceleration = Vector2.ClampMagnitude(steering, maxSpeed);
        // apply our steering value to our current velocity each frame
        velocity += acceleration * Time.deltaTime;
        // update our vechiles position based on our velocity each frame
        transform.position += (Vector3)velocity * Time.deltaTime; // casting our velocity too a Vector3 too update our objects position
    }

    protected virtual void RotateObject(Vector2 direction){
        
        direction.Normalize();
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rotZ -= 90;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }
}
