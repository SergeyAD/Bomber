using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject cameraPoint;
    public GameObject objectPoint;
    public GameObject cameraFirePoint;
    public GameObject objectFirePoint;
    public float speed;
    public bool mainCamera;

    private void Awake()
    {
        mainCamera = true;
    }

    private void Update()
    {
        // переключение между камерами
        if (Input.GetMouseButtonDown(1))
        {
            mainCamera = false;
        }
        if (Input.GetMouseButtonUp(1))
        {
            mainCamera = true;
        }
    }

    private void LateUpdate()
    {
        if (objectPoint != null)
        {
            if (mainCamera)
            {
                transform.position = Vector3.MoveTowards(transform.position, cameraPoint.transform.position, speed * Time.deltaTime);
                transform.LookAt(objectPoint.transform);
            }
                
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, cameraFirePoint.transform.position, speed * 10* Time.deltaTime);
                transform.LookAt(objectFirePoint.transform);
            }
                
            
        }
    }


}
