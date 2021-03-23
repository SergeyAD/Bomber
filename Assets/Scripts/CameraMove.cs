using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject _object;
    private Vector3 _position;
    public float _speed;

    private void Start()
    {
        _position = _object.transform.InverseTransformPoint(transform.position);
    }

    private void LateUpdate()
    {
        if (_object != null)
        {
            var currentPosition = _object.transform.TransformPoint(_position);
            transform.position = Vector3.MoveTowards(transform.position, currentPosition, _speed * Time.deltaTime);
            transform.LookAt(_object.transform);
        }
    }


}
