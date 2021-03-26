using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserFinishScript : MonoBehaviour
{
    public Collider player;

    private void OnTriggerEnter(Collider other)
    {
        if (other = player)
        {
            endLevel(true);
        }
    }

    public void endLevel(bool win)
    {
        Application.Quit();
    }

}
