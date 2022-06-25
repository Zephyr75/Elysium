using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private Vector3 direction;
    
    // Start is called before the first frame update
    void Start()
    {
        direction = transform.up;
        StartCoroutine(DestroyLaser());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * 5 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Mirror"))
        {
            //Up & Right
            if (other.transform.up == direction)
            {
                transform.position = other.transform.position;
                direction = other.transform.right;
            }
            else if (-other.transform.right == direction)
            {
                transform.position = other.transform.position;
                direction = -other.transform.up;
            }
        }
    }

    IEnumerator DestroyLaser()
    {
        yield return new WaitForSeconds(30);
        Destroy(gameObject);
    }

}
