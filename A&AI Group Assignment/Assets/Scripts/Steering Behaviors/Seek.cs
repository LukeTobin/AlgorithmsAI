using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : SteeringBehavior
{   
    GameObject target;

    void Start(){
        if(target == null)
            target = World.Instance.GetTargetObject();
        vechile = GetComponent<Vechile>();
        maxSpeed = vechile.GetMaxSpeed();
    }

    void Update() {
        if(!vechile.steeringMovementOnUpdate) return;
        Steering((Vector2)target.transform.position); // casting our targets position as a Vector2
        RotateObject((Vector2)target.transform.position - (Vector2)transform.position);
    }
}
