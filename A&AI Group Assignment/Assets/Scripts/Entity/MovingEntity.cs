using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingEntity : BaseGameEntity
{
    [Header("Properites")]
    [SerializeField] float maxSpeed = 15f;
    [SerializeField] float mass = 30f;
    [SerializeField] float maxForce = 10f;
    [SerializeField] float radiusSize = 3f;

    public float GetMaxSpeed(){
        return maxSpeed;
    }
    
    public float GetMass(){
        return mass;
    }

    public float GetMaxForce(){
        return maxForce;
    }

    public float GetRadiusSize(){
        return radiusSize;
    }
}
