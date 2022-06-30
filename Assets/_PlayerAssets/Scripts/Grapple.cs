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
    private bool isGrappling = false;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 2;
    }

    void Update()
    {

        
        //Debug.DrawRay(cam.position, cam.forward * maxDistance, Color.red);
        if (isGrappling && player.GetComponent<PlayerMovement>().onGround){
            UpdateJointLength();
        }
        /*
        if (e)
        {
            StartGrapple();
        }
        if (jump && isGrappling)
        {
            StopGrapple();
        }
        */
    }

    void LateUpdate()
    {
        DrawRope();
    }

    void UpdateJointLength(){
        float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

        joint.maxDistance = distanceFromPoint;
        joint.minDistance = distanceFromPoint * .25f;
    }

    void StartGrapple()
    {
        RaycastHit hit;
        //print("1");
        if (Physics.Raycast(cam.position, cam.forward, out hit, maxDistance, grappeableMask))
        {
            //print("2");
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            UpdateJointLength();

            joint.spring = 104.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;

            isGrappling = true;

            lr.enabled = true;

        }
    }

    void DrawRope(){
        if (isGrappling){
            lr.SetPosition(0, hand.position);
            lr.SetPosition(1, grapplePoint);
        }
    }

    void StopGrapple()
    {
        //lr.positionCount = 0;
        player.GetComponent<Rigidbody>().AddForce(player.GetComponent<Rigidbody>().velocity * 50);
        Destroy(joint);
        isGrappling = false;
        lr.enabled = false;
    }

}
