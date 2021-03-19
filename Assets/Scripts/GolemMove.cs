using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GolemMove : MonoBehaviour
{
    public Transform[] _points;
    public float _speed = 0.0f, _distance = 0.0f;
    private int _currentPoint;
    private NavMeshAgent _navMesh;

    void Awake()
    {
        _navMesh = GetComponent<NavMeshAgent>();
        _currentPoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (_points.Length != 0)
        {
            if (Vector3.Distance(transform.position, _points[_currentPoint].position) < _distance)
            {
                if (_currentPoint == _points.Length - 1)
                {
                    _currentPoint = 0;
                }
                else
                {
                    _currentPoint++;
                }
                    
            }
            if (GetComponent<Animator>())
            GetComponent<Animator>().SetBool("Walk", true);
            _navMesh.destination = _points[_currentPoint].position;
        }

        //if (_currentPoint == Points.Length) _currentPoint = 0;
        //
        //float _currentDistance = Vector3.Distance(transform.position, Points[_currentPoint].position);
        //if (_currentDistance <= Distance) _currentPoint++;
        //
        //
        //transform.LookAt(Points[_currentPoint].position);
        //GetComponent<Animator>().SetBool("Walk",true);
        //transform.position = Vector3.MoveTowards(transform.position, Points[_currentPoint].position, Speed * Time.deltaTime);


    }
}
