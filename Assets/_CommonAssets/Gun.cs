using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    private GameObject bullet;
    private float coolDown;
    void Start()
    {
        bullet = GameObject.FindGameObjectWithTag("Bullet");
    }

    void FixedUpdate()
    {
        coolDown -= Time.deltaTime;
        Debug.DrawRay(transform.position, transform.forward * 100, Color.red);
        if (Physics.Raycast(transform.position, transform.forward, 100, LayerMask.GetMask("Player")) && coolDown < 0)
        {
            print("shots fired");
            GameObject newBullet = Instantiate(bullet, transform.position, transform.rotation * new Quaternion(0, 0, 90, 0));
            newBullet.GetComponent<Rigidbody>().AddForce(newBullet.transform.forward*5000);
            coolDown = 0.5f;
        }
    }
}
