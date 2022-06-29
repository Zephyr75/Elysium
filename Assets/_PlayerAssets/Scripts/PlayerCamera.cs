 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private float sensitivity = 1, verticalRotation, horizontalRotation;
    [SerializeField] private int defaultDistance;
    [SerializeField] private int zoomedDistance;

    [SerializeField] private Transform focus, player;
    [SerializeField] private GameObject uiTarget, moveCamera, aimCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.isPaused)
        {
            return;
        }

        horizontalRotation += Input.GetAxis("Mouse X") * sensitivity;
        verticalRotation += Input.GetAxis("Mouse Y") * sensitivity;
        
        verticalRotation = Mathf.Clamp(verticalRotation, -80, 80);
        focus.localEulerAngles = Vector3.left * verticalRotation + Vector3.up * horizontalRotation;
        
        if (transform.GetComponent<PlayerMovement>().CanRotateCamera())
        {
            player.localEulerAngles = new Vector3(0, focus.localEulerAngles.y, 0);
        }

        if (Input.GetMouseButton(2))
        {
            player.localEulerAngles = new Vector3(0, focus.localEulerAngles.y, 0);
            moveCamera.SetActive(false);
            aimCamera.SetActive(true);
            uiTarget.SetActive(true);
        }
        else
        {
            uiTarget.SetActive(false);
            moveCamera.SetActive(true);
            aimCamera.SetActive(false);
        }
        
    }
}
