using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class EchoScript : MonoBehaviour
{
    [SerializeField]
    public Collider player;
    public AudioMixerGroup audioMixer;


    private void OnTriggerEnter(Collider other)
    {
        if (other == player) // не могу понять как включить или отключить эффект эха...
        {
           // audioMixer.audioMixer.
        }
    }



}
