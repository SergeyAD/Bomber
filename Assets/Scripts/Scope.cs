using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scope : MonoBehaviour
{

    public GameObject bullet2point;
    public GameObject bullet2;

    private void Update()
    {
        Vector3 point = Input.mousePosition;
        var newPoint = Camera.main.ScreenToWorldPoint(position: new Vector3(point.x, point.y, z: 280));
        gameObject.transform.LookAt(newPoint);

        //GameObject _bull2 = Instantiate(bullet2, bullet2point.transform.position, bullet2point.transform.rotation);
        //GameObject _bull2 = Instantiate(bullet2, bullet2point.transform.position, bullet2point.transform.rotation);
        //_bull2.GetComponent<BulletMove>()._sender = gameObject;

        //_bull2.GetComponent<Rigidbody>().AddForce(newPoint * 0.2f, ForceMode.Impulse);
    }

}
