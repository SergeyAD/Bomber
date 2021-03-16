using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurrelScript : MonoBehaviour
{
    public float _distance;
    public GameObject _bullet;
    public float _speedRotation;
    public Transform _target;
    public float _pouseFire;
    public float _powerFire;

    private float _pouse;
    private bool _canFire = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, _target.transform.position) < _distance)
        {
            Vector3 relativePos = _target.position - transform.position;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, relativePos, _speedRotation*Time.deltaTime, 0F);

            Quaternion rotation = Quaternion.LookRotation(newDir);
            transform.rotation = rotation;


            if (Vector3.Angle(transform.forward,relativePos.normalized) < 1)
            {

                relativePos = _target.position - transform.GetChild(0).transform.position;
                newDir = Vector3.RotateTowards(transform.GetChild(0).transform.forward, relativePos, _speedRotation * Time.deltaTime, 0F);
                rotation = Quaternion.LookRotation(newDir);
                transform.GetChild(0).transform.rotation = rotation;

                print("Boom!");

                if (_canFire == false) _pouse += Time.deltaTime;
                if (_pouse > _pouseFire) _canFire = true;


                if (_canFire == true)
                {

                    GameObject bull = Instantiate(_bullet, transform.GetChild(0).GetChild(0).gameObject.transform.position, transform.rotation);
                    bull.GetComponent<Rigidbody>().AddForce(transform.GetChild(0).GetChild(0).gameObject.transform.forward * _powerFire, ForceMode.Impulse);
                    
                    _canFire = false;
                    _pouse = 0;
                }


            }

        }


    }
}
