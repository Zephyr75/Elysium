using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    [SerializeField] private GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject newBullet = Instantiate(bullet, transform.position, transform.rotation * new Quaternion(0, 0, 90, 0));
            newBullet.GetComponent<Rigidbody>().AddForce(newBullet.transform.forward*5000);
        }
    }
}
