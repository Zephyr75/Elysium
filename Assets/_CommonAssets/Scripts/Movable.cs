using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movable : MonoBehaviour
{
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";

    protected float horizontalInput, verticalInput;
    
    protected bool shift, ctrl, space, a, e, mouse_0, mouse_1;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void GetInput()
    {
        shift = Input.GetKeyDown(KeyCode.V);
        ctrl = Input.GetKeyDown(KeyCode.C);
        space = Input.GetKey(KeyCode.Space);
        a = Input.GetKeyDown(KeyCode.A);
        e = Input.GetKey(KeyCode.E);
        mouse_0 = Input.GetMouseButtonDown(0);
        mouse_1 = Input.GetMouseButtonDown(1);
        horizontalInput = Input.GetAxis(Horizontal);
        verticalInput = Input.GetAxis(Vertical);
    }
}
