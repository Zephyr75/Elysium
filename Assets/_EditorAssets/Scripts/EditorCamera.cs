using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorCamera : MonoBehaviour
{
    private int view, index;
    private bool resetCam;
    private Vector3 rotation;
    private float movementSpeed = 10, sensitivity = 5, verticalRotation, horizontalRotation, offsetX, offsetY;
    private List<Vector3> positions = new List<Vector3>();
    private List<Vector3> rotations = new List<Vector3>();
    private Dictionary<int, int[]> indexTab = new Dictionary<int, int[]>();
    
    // Start is called before the first frame update
    void Start()
    {
        //Front ========== 0
        positions.Add(new Vector3(0, 0, -10));
        rotations.Add(new Vector3(0, 0, 0));
        //Back ========== 1
        positions.Add(new Vector3(0, 0, 10));
        rotations.Add(new Vector3(0, 180, 0));
        //Up ========== 2
        positions.Add(new Vector3(0, 10, 0));
        rotations.Add(new Vector3(90, 0, 0));
        //Down ========== 3
        positions.Add(new Vector3(0, -10, 0));
        rotations.Add(new Vector3(-90, 0, 0));
        //Right ========== 4
        positions.Add(new Vector3(10, 0, 0));
        rotations.Add(new Vector3(0, -90, 0));
        //Left ========== 5
        positions.Add(new Vector3(-10, 0, 0));
        rotations.Add(new Vector3(0, 90, 0));
        //Indexes ========== 0, 1, 2, 3, 4, 5
        indexTab.Add(5, new int[] {0, 0, 0, 0, 0, 0});
        indexTab.Add(6, new int[] {4, 5, 4, 4, 1, 0});
        indexTab.Add(4, new int[] {5, 4, 5, 5, 0, 1});
        indexTab.Add(8, new int[] {2, 2, 1, 0, 2, 2});
        indexTab.Add(2, new int[] {3, 3, 0, 1, 3, 3});
    }

    // Update is called once per frame
    void Update()
    {
        FixedView();
        Move();
        Rotate();
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetCamera(rotation);
        }
    }

    void Rotate()
    {
        horizontalRotation += Input.GetAxis("Mouse X");
        verticalRotation += Input.GetAxis("Mouse Y");
        //verticalRotation = Mathf.Clamp(verticalRotation, -90, 90);
        //transform.eulerAngles = Vector3.left * verticalRotation + Vector3.up * horizontalRotation;
        transform.localEulerAngles = Vector3.left * (verticalRotation + offsetY) + Vector3.up * (horizontalRotation + offsetX) + rotation;
    }

    void Move()
    {
        if (Input.GetKey(KeyCode.Z) || Input.mouseScrollDelta.y == 1)
        {
            transform.position += transform.forward * Time.deltaTime * movementSpeed;
        }
        if (Input.GetKey(KeyCode.S) || Input.mouseScrollDelta.y == -1)
        {
            transform.position -= transform.forward * Time.deltaTime * movementSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * Time.deltaTime * movementSpeed;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.position -= transform.right * Time.deltaTime * movementSpeed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += transform.up * Time.deltaTime * movementSpeed;
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.position -= transform.up * Time.deltaTime * movementSpeed;
        }
    }
    
    public void ResetCamera(Vector3 rotation)
    {
        offsetX = -horizontalRotation;
        offsetY = -verticalRotation;
        this.rotation = rotation;
    }

    void FixedView()
    {
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            resetCam = true;
            index = 5;
        }
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            resetCam = true;
            index = 6;
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            resetCam = true;
            index = 4;
        }
        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            resetCam = true;
            index = 8;
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            resetCam = true;
            index = 2;
        }
        if (resetCam)
        {
            if (!positions.Contains(transform.position))
            {
                view = indexTab[index][0];
            }
            else
            {
                switch (view)
                {
                    case 0: view = indexTab[index][0]; break;
                    case 1: view = indexTab[index][1]; break;
                    case 2: view = indexTab[index][2]; break;
                    case 3: view = indexTab[index][3]; break;
                    case 4: view = indexTab[index][4]; break;
                    default: view = indexTab[index][5]; break;
                }
            }
            transform.position = positions[view];
            ResetCamera(rotations[view]);
            resetCam = false;
        }
    }
}
