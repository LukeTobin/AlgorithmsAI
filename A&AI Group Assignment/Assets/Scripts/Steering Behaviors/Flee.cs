using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee : SteeringBehavior
{
    GameObject target;

    void Start()
    {
        if(target == null)
            target = GlobalManager.Instance.GetTargetObject();
        vechile = GetComponent<Vechile>();
        maxSpeed = vechile.GetMaxSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        Steering(-(Vector2)target.transform.position);
        RotateObject(-(Vector2)target.transform.position - (Vector2)transform.position);
    }
}
