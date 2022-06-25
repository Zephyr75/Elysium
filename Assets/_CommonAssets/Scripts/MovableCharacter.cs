using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableCharacter : Movable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    protected virtual void Move()
    {
        print("Move method not overriden");
    }
    
    protected virtual void Attack()
    {
        print("Move method not overriden");
    }
}
