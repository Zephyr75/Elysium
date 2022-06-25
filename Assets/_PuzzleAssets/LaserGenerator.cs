using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGenerator : MonoBehaviour
{
    private bool spawnNew = true;
    [SerializeField] private GameObject laser;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnNew)
        {
            StartCoroutine(SpawnLaser());
        }
    }

    IEnumerator SpawnLaser()
    {
        spawnNew = false;
        GameObject.Instantiate(laser, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(4.9f);
        spawnNew = true;
    }
}
