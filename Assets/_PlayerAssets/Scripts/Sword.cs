using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private bool isAttacking = false;
    [SerializeField] private Collider playerCollider;
    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreCollision(transform.GetComponent<Collider>(), playerCollider);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Enemy") && isAttacking)
        {
            print("get rekt");
            col.gameObject.GetComponent<Rigidbody>().AddForce(-col.transform.forward*1000);
        }
    }

    public IEnumerator Attack()
    {
        isAttacking = true;
        yield return new WaitForSeconds(1.2f);
        isAttacking = false;
    }
}
