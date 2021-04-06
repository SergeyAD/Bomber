using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryScript : MonoBehaviour
{
    //private LineRenderer _lineRendererComponent;


    //private void Start()
    //{
    //    _lineRendererComponent = GetComponent<LineRenderer>();
    //}

    //public void ShowTrajectory( Vector3 _origin, Vector3 _speed)
    //{
    //    _lineRendererComponent.enabled = true;

    //    Vector3[] _points = new Vector3[100];
    //    _lineRendererComponent.positionCount = _points.Length;

    //    for (var i = 0; i < _points.Length; i++)
    //    {
    //        float _time = i * 0.1f;

    //        _points[i] = _origin + _speed* 10 * _time + Physics.gravity*_time*_time/2f; 
    //        if (_points[i].y < -2 )
    //        {
    //            _lineRendererComponent.positionCount = i+1;
    //            break;
    //        }
    //    }

    //    _lineRendererComponent.SetPositions(_points);
       

    //}

    //public void HideTrajectory()
    //{
    //    _lineRendererComponent.enabled = false;
    //}


}
