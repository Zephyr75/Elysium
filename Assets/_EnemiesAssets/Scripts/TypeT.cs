using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeT : MonoBehaviour
{
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < 100)
        {
            Quaternion targetRotation = Quaternion.LookRotation(player.position - transform.position, Vector3.up);
            targetRotation.x = 0;
            targetRotation.z = 0;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * 100);
        }
    }
}
