using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class EchoScript : MonoBehaviour
{
    [SerializeField]
    public Collider player;
    [SerializeField]
    public AudioReverbZone _audioReverbZone;


    private void OnTriggerEnter(Collider other)
    {
        if (other == player) 
        {
            _audioReverbZone.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == player)
        {
            _audioReverbZone.enabled = false;
        }
    }



}
