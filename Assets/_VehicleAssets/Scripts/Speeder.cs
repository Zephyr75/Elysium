using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speeder : Vehicle
{
    private RaycastHit hit;
    private readonly List<Vector3> hoverPositions = new List<Vector3>(4);
    
    private readonly Vector3 m_EulerAngleVelocity = new Vector3(0, 0, 0);
    [SerializeField] private float power, maxSpeed;
    
    void Start()
    {
        hoverPositions.Add(Vector3.zero);
        hoverPositions.Add(Vector3.zero);
        hoverPositions.Add(Vector3.zero);
        hoverPositions.Add(Vector3.zero);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        float distance = 1f;
        hoverPositions[0] = transform.position + transform.forward * distance + transform.right * distance;
        hoverPositions[1] = transform.position + transform.forward * distance - transform.right * distance;
        hoverPositions[2] = transform.position - transform.forward * distance + transform.right * distance;
        hoverPositions[3] = transform.position - transform.forward * distance - transform.right * distance;
        
        
        Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.fixedDeltaTime);
        transform.GetComponent<Rigidbody>().MoveRotation(transform.GetComponent<Rigidbody>().rotation * deltaRotation);

        //canHover = Physics.Raycast(transform.position, -transform.up, out hit, 3f);
        bool planning = true;
        for (int i = 0; i < 4; i++)
        {
            if (Physics.Raycast(hoverPositions[i], -transform.up, out hit, 3f))
            {
                GetComponent<Rigidbody>().AddForceAtPosition((3f - hit.distance) * transform.up * power, hoverPositions[i]);
                planning = false;
            }
        }
        if (planning)
        {
            Quaternion target = new Quaternion(0, GetComponent<Rigidbody>().rotation.y, 0, 1);
            GetComponent<Rigidbody>().rotation =
                Quaternion.Lerp(GetComponent<Rigidbody>().rotation, target, Time.deltaTime);
        }
        Debug.DrawRay(hoverPositions[0], -transform.up, Color.red);
        Debug.DrawRay(hoverPositions[1], -transform.up, Color.red);
        Debug.DrawRay(hoverPositions[2], -transform.up, Color.red);
        Debug.DrawRay(hoverPositions[3], -transform.up, Color.red);

        
        Vector3 angularVelocity = GetComponent<Rigidbody>().angularVelocity;
        print(angularVelocity.x + " " + angularVelocity.z);
        float clampValue = .1f;
        GetComponent<Rigidbody>().angularVelocity = new Vector3(Mathf.Clamp(angularVelocity.x, -clampValue, clampValue), angularVelocity.y * .9f, Mathf.Clamp(angularVelocity.x, -clampValue, clampValue));
        //transform.GetComponent<Rigidbody>().angularVelocity *= .9f;
        
        Vector3 velocity = GetComponent<Rigidbody>().velocity;
        GetComponent<Rigidbody>().velocity = new Vector3(velocity.x * .99f, velocity.y * .9f, velocity.z * .99f);
        if (transform.GetComponent<Rigidbody>().velocity.magnitude < 2000){
            GetComponent<Rigidbody>().AddForce(verticalInput * transform.forward * 20);
        }
        GetComponent<Rigidbody>().AddTorque(transform.up * horizontalInput * 50);
        
    }
}
