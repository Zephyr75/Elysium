using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    private LineRenderer lr;
    private Vector3 grapplePoint;
    [SerializeField] private LayerMask grappeableMask;
    [SerializeField] private Transform hand, cam, player;
    private float maxDistance = 100f;
    private SpringJoint joint;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        
        Debug.DrawRay(cam.position, cam.forward * maxDistance, Color.red);
        if (Input.GetMouseButtonDown(0))
        {
            StartGrapple();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StopGrapple();
        }
    }

    void LateUpdate()
    {
        DrawRope();
    }

    void StartGrapple()
    {
        RaycastHit hit;
        print("1");
        if (Physics.Raycast(cam.position, cam.forward, out hit, maxDistance, grappeableMask))
        {
            print("2");
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            joint.maxDistance = distanceFromPoint * .8f;
            joint.minDistance = distanceFromPoint * .25f;

            joint.spring = 104.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;

            lr.positionCount = 2;

        }
    }

    void DrawRope(){
        //if (joint != null) return;
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, grapplePoint);
    }

    void StopGrapple()
    {
        lr.positionCount = 0;
        player.GetComponent<Rigidbody>().AddForce(player.up * 600);
        Destroy(joint);
    }

}
