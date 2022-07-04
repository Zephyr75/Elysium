using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Vehicle : MonoBehaviour
{
    [SerializeField] protected InputActionMap input;
    
    protected float verticalInput, horizontalInput;
    
    void Start()
    {

        //input["Move"].performed += ctx => Move(ctx);
    }

    void OnEnable()
    {
        input.Enable();
    }

    void OnDisable()
    {
        input.Disable();
    }

    protected virtual void FixedUpdate(){
        Move(input["Move"].ReadValue<Vector2>());
    }

    void Move(Vector2 inputDirection){
        horizontalInput = inputDirection.x;
        verticalInput = inputDirection.y;
    }

}
