using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private float sensitivity = 1, verticalRotation, horizontalRotation;
    [SerializeField] private int defaultDistance;
    [SerializeField] private int zoomedDistance;

    [SerializeField] private Transform focusPlayer, cameraPlayer, modelPlayer;
    [SerializeField] private GameObject uiTarget;
    [SerializeField] private bool isPlayerCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalRotation += Input.GetAxis("Mouse X") * sensitivity;
        verticalRotation += Input.GetAxis("Mouse Y") * sensitivity;
        
        verticalRotation = Mathf.Clamp(verticalRotation, -80, 80);
        //focus.position = transform.position + transform.up * 2;
        focusPlayer.localEulerAngles = Vector3.left * verticalRotation + Vector3.up * horizontalRotation;
        
        if (isPlayerCamera && transform.GetComponent<PlayerMovement>().CanRotateCamera())
        {
            modelPlayer.localEulerAngles = new Vector3(0, focusPlayer.localEulerAngles.y, 0);
        }
        cameraPlayer.localEulerAngles = focusPlayer.localEulerAngles;
        if (Input.GetMouseButton(2))
        {
            modelPlayer.localEulerAngles = new Vector3(0, focusPlayer.localEulerAngles.y, 0);
            uiTarget.SetActive(true);
            cameraPlayer.position = focusPlayer.position - focusPlayer.forward * zoomedDistance + focusPlayer.right;
        }
        else
        {
            uiTarget.SetActive(false);
            cameraPlayer.position = focusPlayer.position - focusPlayer.forward * defaultDistance;
        }
        
    }
}
