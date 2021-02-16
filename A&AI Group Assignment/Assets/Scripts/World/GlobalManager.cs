using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalManager : MonoBehaviour
{
    public static GlobalManager Instance;
    [SerializeField] GameObject objectToSeek = null;

    void Awake(){
        Instance = this;
    }

    void Update()
    {
        // change the current location of objectToSeek to where you click on the screen
        if(Input.GetMouseButtonDown(0)){
            objectToSeek.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            objectToSeek.transform.position = new Vector3(objectToSeek.transform.position.x, objectToSeek.transform.position.y, 0);  
        }
    }

    public GameObject GetTargetObject(){
        return objectToSeek;
    }
}
