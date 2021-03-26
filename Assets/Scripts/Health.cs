using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float _health;
    public int _lives;
    public int _gamePoints;
    private bool alive;
    private float pouse;

    private void Awake()
    {
        alive = true;
    }


    private void Update()
    {
        if (_health <= 0 && alive)
        {
            if (GetComponent<Animator>())
            {
                GetComponent<Animator>().SetBool("Die", true);
                pouse = 2;
            }
            pouse = 1;

            alive = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<MainMove>().gamePoints += _gamePoints;
            Destroy(gameObject, pouse);
        }
    }


}
