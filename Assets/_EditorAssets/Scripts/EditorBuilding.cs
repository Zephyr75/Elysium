using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorBuilding : MonoBehaviour
{
    private GameObject chosenBlock, previewBlock;
    private RaycastHit hit, hitLink;
    private Vector3 position;
    private Quaternion rotation;
    private List<GameObject> placedTab;
    private List<Vector3> around = new List<Vector3>();
    
    [SerializeField] private GameObject[] moduleTab = new GameObject[10];
    [SerializeField] private Material transparent;
    [SerializeField] private GameObject machine;
    [SerializeField] private Camera cameraEditor;
    

    // Start is called before the first frame update
    void Start()
    {
        rotation = Quaternion.identity;
        position = Vector3.zero;
        chosenBlock = moduleTab[0];
        around.Add(Vector3.forward);
        around.Add(-Vector3.forward);
        around.Add(Vector3.right);
        around.Add(-Vector3.right);
        around.Add(Vector3.up);
        around.Add(-Vector3.up);
    }

    // Update is called once per frame
    void Update()
    {
        Physics.Raycast(cameraEditor.ScreenPointToRay(Input.mousePosition), out hit, 10);
        
        ChooseBlock();
        Preview();
        if (Input.GetMouseButtonDown(0))
        {
            PlaceBlock();
        }

        if (SystemController.StartMachine)
        {
            StartMachine();
        }
    }
    
    private void Preview()
    {
        Destroy(previewBlock);
        position = PreviewPosition();
        previewBlock = GameObject.Instantiate(chosenBlock, position, rotation);
        foreach (Renderer child in previewBlock.GetComponentsInChildren<Renderer>())
        {
            child.material = transparent;
        }
    }

    void PlaceBlock()
    {
        GameObject placedBlock = GameObject.Instantiate(chosenBlock, position, rotation);
        placedBlock.transform.parent = machine.transform;
        //placedBlock.GetComponent<Collider>().enabled = true;
        placedBlock.transform.parent = machine.transform;
        placedBlock.GetComponent<Collider>().enabled = true;
        foreach (Collider col in placedBlock.GetComponentsInChildren<Collider>())
        {
            if (!col.gameObject.CompareTag("PlayOnly"))
            {
                col.enabled = true;
            }
        }

        foreach (Vector3 direction in around)
        {
            if (Physics.Raycast(position, direction, out hitLink, 1))
            {
                FixedJoint joint = placedBlock.AddComponent<FixedJoint>();
                joint.connectedBody = hitLink.rigidbody;
                joint.breakForce = 3000;
            }
        }
    }

    void StartMachine()
    {
        foreach (Rigidbody rb in machine.GetComponentsInChildren<Rigidbody>())
        {
            rb.useGravity = true;
        }
        

        foreach (Collider col in machine.GetComponentsInChildren<Collider>())
        {
            if (!col.gameObject.CompareTag("EditOnly"))
            {
                col.enabled = true;
            }
            else
            {
                col.enabled = false;
            }
        }
        SystemController.StartMachine = false;
    }
    
    private Vector3 PreviewPosition()
    {
        float x = 0;
        float y = 0;
        float z = 0;
        if (hit.transform != null)
        {
            x = Mathf.Round(hit.transform.position.x) + Mathf.Round(hit.normal.x);
            y = Mathf.Round(hit.transform.position.y) + Mathf.Round(hit.normal.y);
            z = Mathf.Round(hit.transform.position.z) + Mathf.Round(hit.normal.z);
        }
        return new Vector3(x, y, z);
    }

    void ChooseBlock()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            chosenBlock = moduleTab[0];
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            chosenBlock = moduleTab[1];
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            chosenBlock = moduleTab[2];
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            chosenBlock = moduleTab[3];
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            chosenBlock = moduleTab[4];
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            chosenBlock = moduleTab[5];
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            chosenBlock = moduleTab[6];
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            chosenBlock = moduleTab[7];
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            chosenBlock = moduleTab[8];
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            chosenBlock = moduleTab[9];
        }
    }
}
