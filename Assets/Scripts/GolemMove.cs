using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemMove : MonoBehaviour
{
    public Transform[] Points;
    public float Speed = 0.0f, Distance = 0.0f;
    private int _currentPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (_currentPoint == Points.Length) _currentPoint = 0;

        float _currentDistance = Vector3.Distance(transform.position, Points[_currentPoint].position);
        if (_currentDistance <= Distance) _currentPoint++;


        transform.LookAt(Points[_currentPoint].position);
        GetComponent<Animator>().SetBool("Walk",true);
        transform.position = Vector3.MoveTowards(transform.position, Points[_currentPoint].position, Speed * Time.deltaTime);


    }
}
