using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrive : SteeringBehavior
{
    GameObject target;
    float radius;

    void Start(){
        if(target == null)
            target = GlobalManager.Instance.GetTargetObject();
        vechile = GetComponent<Vechile>();
        maxSpeed = vechile.GetMaxSpeed();
        radius = vechile.GetRadiusSize();
    }

    void Update() {
        Steering(target.transform.position);
        RotateObject((Vector2)target.transform.position - (Vector2)transform.position);
    }

    protected override void Steering(Vector2 targetPostion)
    {
        // Get our current distance from the target compared to our current position
        Vector2 distance = (targetPostion - (Vector2)transform.position); //transform.position is a vector3, so we cast it to a Vector2 since we only need two axises
        
        // Normalize our distance. Keeps the Vectors direction, but makes it length 1
        distance.Normalize();
        
        // Get a float of our current distance
        float distanceFromTarget = Vector2.Distance(targetPostion, transform.position);
        
        // Get the desired velocity towards our target, based on distance
        Vector2 desiredVelocity;
        if(distanceFromTarget < radius)
            desiredVelocity = distance.normalized * distanceFromTarget;
        else
            desiredVelocity = distance.normalized * maxSpeed;
        
        // Setup our steering vector based on our current velocity and desired velocity
        Vector2 steering = desiredVelocity - velocity;

        // Truncate our steering vector based around our maxSpeed
        acceleration = Vector2.ClampMagnitude(steering, maxSpeed);

        // apply our steering value to our current velocity each frame
        velocity += acceleration * Time.deltaTime;
        
        // update our vechiles position based on our velocity each frame
        transform.position += (Vector3)velocity * Time.deltaTime; // casting our velocity too a Vector3 too update our objects position
    }
}
