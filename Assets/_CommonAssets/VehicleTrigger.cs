using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VehicleTrigger : MonoBehaviour
{
    [SerializeField] private GameObject camVehicle, player;
    [SerializeField] private Rover rover;

    private bool inTrigger, inVehicle;

    [SerializeField] private InputActionMap input;
    
    void Start()
    {
        input["Enter"].performed += ctx => Enter(ctx);
    }

    void OnEnable()
    {
        input.Enable();
    }

    void OnDisable()
    {
        input.Disable();
    }

    void Enter(InputAction.CallbackContext context){
        if (inVehicle)
        {
            player.SetActive(true);
            camVehicle.SetActive(false);
            rover.enabled = false;
        }
        else
        {
            player.SetActive(false);
            camVehicle.SetActive(true);
            rover.enabled = true;
        }
        inVehicle = !inVehicle;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")){
            inTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")){
            inTrigger = false;
        }
    }
}
