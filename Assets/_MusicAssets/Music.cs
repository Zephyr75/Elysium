using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Music : MonoBehaviour
{
    [SerializeField] private AudioSource macarena;
    [SerializeField] private PlayerMovement player;
    [SerializeField] private GameObject confetti;
    [SerializeField] private Volume postProcessing;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            StartCoroutine(PlayMacarena());
        }
    }

    IEnumerator PlayMacarena()
    {
        StartCoroutine(player.PlayAnimation("Macarena", 30));
        yield return new WaitForSeconds(.2f);
        //postProcessing.profile.components[0].parameters[0].SetValue(VolumeParameter());
        confetti.SetActive(true);
        macarena.Play();
        yield return new WaitForSeconds(30);
        macarena.Stop();
    }
}
