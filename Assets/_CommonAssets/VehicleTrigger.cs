using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleTrigger : MonoBehaviour
{
    [SerializeField] private GameObject player, vehicle, cam;

    private bool inVehicle;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")) print("Enter vehicle:");
        if (Input.GetKeyDown(KeyCode.E) && other.CompareTag("Player"))
        {
            print("Entered");
            inVehicle = !inVehicle;
            cam.SetActive(inVehicle);
            player.SetActive(!inVehicle);
            foreach (MonoBehaviour script in vehicle.GetComponents<MonoBehaviour>())
            {
                script.enabled = inVehicle;
            }
        }
    }
}
