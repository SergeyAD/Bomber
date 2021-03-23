using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public GameObject key;
    public bool open;

    void Update()
    {
        if (open)
            gameObject.SetActive(false);
    }
}
