 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    //TODO Settings: add sensitivity
    private float sensitivity = 2f, verticalRotation, horizontalRotation;
    [SerializeField] private int defaultDistance;
    [SerializeField] private int zoomedDistance;

    [SerializeField] private Transform focus, player;
    [SerializeField] private GameObject uiTarget, moveCamera, aimCamera;

    
    [SerializeField] private InputActionMap input;

    void Start(){
        
        input["Aim"].performed += ctx => StartAiming(ctx);
        input["Aim"].canceled += ctx => StopAiming(ctx);
    }

    void OnEnable()
    {
        input.Enable();
    }

    void OnDisable()
    {
        input.Disable();
    }
    
    void FixedUpdate()
    {
        UpdateCamera(input["Move"].ReadValue<Vector2>());
    }

    
    public void UpdateCamera(Vector2 movementDirection)
    {
        if (GameManager.isPaused)
        {
            return;
        }
        
        horizontalRotation += movementDirection.x * sensitivity;
        verticalRotation += movementDirection.y * sensitivity;
        
        verticalRotation = Mathf.Clamp(verticalRotation, -80, 80);
        focus.localEulerAngles = Vector3.left * verticalRotation + Vector3.up * horizontalRotation;
    }

    void StartAiming(InputAction.CallbackContext context){
        moveCamera.SetActive(false);
        aimCamera.SetActive(true);
    }

    void StopAiming(InputAction.CallbackContext context){
        moveCamera.SetActive(true);
        aimCamera.SetActive(false);
    }
}
