using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [Header("Information")]
    [SerializeField] Vector2 coord;
    public int g; 
    public int h;
    public int f; // g: distance from start to end, h: huersitc distance
    public bool unwalkable = false;
    public Node predecessor;


    // private
    SpriteRenderer spriteRenderer;
    bool spriteEnabled = false;

    void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void EnableSprite(){
        if(spriteEnabled){
            spriteRenderer.enabled = true;
            spriteEnabled = false;
        }else{
            spriteRenderer.enabled = false;
            spriteEnabled = true;
        }
    }
}
