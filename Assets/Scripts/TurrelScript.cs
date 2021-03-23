using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurrelScript : MonoBehaviour
{
    public float distance;
    public GameObject bullet;
    public float speedRotation;
    public Transform target;
    public float pouseFire;
    public float powerFire;
    public GameObject barrel;
    public GameObject firePoint;

    private float _pouse;
    private bool _canFire = true;


    void Update()
    {
        if (Vector3.Distance(transform.position, target.transform.position) < distance)
        {
            Vector3 relativePos = target.position - transform.position;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, relativePos, speedRotation*Time.deltaTime, 0F);

            Quaternion rotation = Quaternion.LookRotation(newDir);
            transform.rotation = rotation;


            if (Vector3.Angle(transform.forward,relativePos.normalized) < 1)
            {

                relativePos = target.position - barrel.transform.position;
                newDir = Vector3.RotateTowards(barrel.transform.forward, relativePos, speedRotation * Time.deltaTime, 0F);
                rotation = Quaternion.LookRotation(newDir);
                barrel.transform.rotation = rotation;

                print("Boom!");

                if (_canFire == false) _pouse += Time.deltaTime;
                if (_pouse > pouseFire) _canFire = true;


                if (_canFire == true)
                {

                    GameObject bull = Instantiate(bullet, firePoint.transform.position, transform.rotation);
                    bull.GetComponent<Rigidbody>().AddForce(firePoint.transform.forward * powerFire, ForceMode.Impulse);
                    
                    _canFire = false;
                    _pouse = 0;
                }


            }

        }


    }
}
