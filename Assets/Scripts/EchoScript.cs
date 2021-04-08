using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class EchoScript : MonoBehaviour
{
    [SerializeField]
    public Collider player;
    [SerializeField]
    public AudioReverbZone AudioReverbZone;


    private void OnTriggerEnter(Collider other)
    {
        if (other == player) 
        {
            AudioReverbZone.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == player)
        {
            AudioReverbZone.enabled = false;
        }
    }



}
