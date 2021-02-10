using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseGameEntity : MonoBehaviour
{
    [Header("Global Data")]
    public Vector2 position;

    // private variables
    SpriteRenderer spriteRenderer;

    void Start(){
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public virtual void Render(Sprite sprite, Color color){
        if(spriteRenderer != null){
            spriteRenderer.sprite = sprite;
            spriteRenderer.color = color;
        }
    }
}
