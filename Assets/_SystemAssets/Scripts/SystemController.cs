using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemController : MonoBehaviour
{
    private static bool inEditor, startMachine;
    
    [SerializeField] private GameObject playerCamera, editorCamera, speederCamera;
    
    public enum Direction
    {
        RIGHT, LEFT
    }
    
    // Start is called before the first frame update
    void Start()
    {
        inEditor = false;
        playerCamera.SetActive(true);
        editorCamera.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (inEditor)
            {
                playerCamera.SetActive(true);
                editorCamera.SetActive(false);
                startMachine = true;
            }
            else
            {
                playerCamera.SetActive(false);
                editorCamera.SetActive(true);
            }

            inEditor = !inEditor;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            playerCamera.SetActive(false);
            editorCamera.SetActive(false);
            speederCamera.SetActive(true);
        }
    }

    public static bool StartMachine
    {
        get => startMachine;
        set => startMachine = value;
    }
}
