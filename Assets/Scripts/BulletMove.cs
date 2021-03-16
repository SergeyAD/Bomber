using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float _health;
    public float _timerBul;
    public GameObject _boom;
    public GameObject _sender;


    private Vector3 _vector;
    private Vector3 _positionPrev;
    private Vector3 _positionNext;
    private float _timer;
    Animator m_Animator;

    private void Start()
    {
        _timer = 0;
        
    }


    private void Update()
    {
        
        
        
        if (transform.position.y < -1)
        {
            Destroy(gameObject,3);
        }
        
        if (_timer > _timerBul)
        {
            
            Destroy(gameObject, 3);
        }
        _timer += Time.deltaTime;

        

    }

    private void FixedUpdate()
    {
        if (_positionPrev == gameObject.transform.position)
        {
            
            Destroy(gameObject, 3);
        }
        else
        {
            _positionPrev = gameObject.transform.position;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != _sender.GetComponent<Collider>())
        {
            var _boomer = Instantiate(_boom, gameObject.transform.position, gameObject.transform.rotation);
            if (other.GetComponent<Health>())
            {
                other.GetComponent<Health>()._health -= _health;
                if (other.GetComponent<Health>()._health <= 0)
                {
                    if (other.gameObject.GetComponent<Animator>())
                    {

                        other.gameObject.GetComponent<Animator>().SetBool("Die", true);
                        Destroy(other.gameObject, 2);
                    }
                    else Destroy(other.gameObject, 1);

                }
            }
            

            Destroy(gameObject);
            Destroy(_boomer, 1);
        }
    }


}
