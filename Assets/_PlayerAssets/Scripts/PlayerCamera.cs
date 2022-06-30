 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    //TODO Settings: add sensitivity
    private float sensitivity = 1f, verticalRotation, horizontalRotation;
    [SerializeField] private int defaultDistance;
    [SerializeField] private int zoomedDistance;

    [SerializeField] private Transform focus, player;
    [SerializeField] private GameObject uiTarget, moveCamera, aimCamera;

    
    [SerializeField] private InputActionMap input;

    void OnEnable()
    {
        input.Enable();
    }

    void OnDisable()
    {
        input.Disable();
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
    
    void FixedUpdate()
    {
        
        UpdateCamera(input["Move"].ReadValue<Vector2>());

        /*if (mouse_2)
        {
            moveCamera.SetActive(false);
            aimCamera.SetActive(true);
        }
        else
        {
            uiTarget.SetActive(false);
            moveCamera.SetActive(true);
            aimCamera.SetActive(false);
            uiTarget.SetActive(true);
        }*/
        
    }
}
