using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TypeG : MonoBehaviour
{
    [SerializeField] private Transform main;
    [SerializeField] private Transform[] targets_1 = new Transform[4];
    [SerializeField] private Transform[] targets_2 = new Transform[4];
    [SerializeField] private float[] steps = new float[4];
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            steps[i] = -1;
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float temp = (Mathf.Sin(Time.time * 5)) * 5;
        Vector3 offset = new Vector3(temp - 90, 0, temp);
        //print(Time.fixedDeltaTime + " " + offset.x + " " + offset.y + " " + offset.z);
        main.localEulerAngles = offset;
        transform.position += transform.forward * Time.deltaTime * 8;
        for (int i = 0; i < 4; i++)
        {
            if (steps[i] > -1)
            {
                steps[i] += 5 * Time.fixedDeltaTime;
                targets_2[i].transform.position = Vector3.Lerp(targets_2[i].transform.position,targets_1[i].transform.position, steps[i]);
            }
            if (Vector3.Distance(targets_1[i].position, targets_2[i].position) > 5)
            {
                StartCoroutine(UpdatePosition(i));
            }
        }
    }

    IEnumerator UpdatePosition(int i)
    {
        steps[i] = 0;
        yield return new WaitForSeconds(.2f);
        steps[i] = -1;
    }
}