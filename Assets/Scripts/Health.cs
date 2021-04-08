using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health;
    public int lives;
    public int gamePoints;
    public GameObject dieObject;
    
    private bool _alive = true;
    private float _pouse;

    
    private void Update()
    {
        if (health <= 0 && _alive)
        {
            if (GetComponent<Animator>())
            {
                GetComponent<Animator>().SetBool("Die", true);
                _pouse = 2;
            }
            _pouse = 1;

            _alive = false;
            //GameObject.FindGameObjectWithTag("Player").GetComponent<MainMove>().gamePoints += gamePoints;
            PlayerManager.ChangeGamePoint(gamePoints);
            if (dieObject != null)
            {
                var _dieObject = Instantiate(dieObject, gameObject.transform.position, gameObject.transform.rotation);
            }
            Destroy(gameObject, _pouse);

            
          
        }
    }


}
