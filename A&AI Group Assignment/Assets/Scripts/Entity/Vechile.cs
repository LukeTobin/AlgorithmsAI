using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vechile : MovingEntity
{
    [Header("Vechile Properties")]
    [SerializeField] Color vechileColor = new Color(0, 0, 0, 1);
    public bool steeringMovementOnUpdate = true;
}
