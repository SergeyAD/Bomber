using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GolemMove : MonoBehaviour
{
    public Transform[] _points;
    public float distanceHunt;
    public Transform target;
    public float attackdistance;
    public float attackPower;
    public float attackPouse;
    
    
    private bool canAttack = true;
    private int _currentPoint;
    private NavMeshAgent _navMesh;
    private bool hunt;
 
    private float pouseHunt;
    private float pouse;

    void Awake()
    {
        _navMesh = GetComponent<NavMeshAgent>();
        _currentPoint = 0;
        hunt = false;
        pouseHunt = 0;

    }

    
    void Update()
    {
        // игрок в зоне видимости
        if (target != null && Vector3.Distance(transform.position, target.transform.position) < distanceHunt)
        {
            hunt = true;
            if (GetComponent<Animator>())
                GetComponent<Animator>().SetBool("Walk", true);
            _navMesh.destination = target.position;
            Debug.Log("Hunt!");
        }

        // потеряли игрока из виду
        if (hunt == true && target != null && Vector3.Distance(transform.position, target.transform.position) > distanceHunt)
        {
            Debug.Log("Lost!");
            pouseHunt++;
            if (pouseHunt > 10)
            {
                hunt = false;
                Debug.Log("Патрулировать!");
            }
                
        }



        // просто патрулирование
        if (!hunt)
        {
            if (_points.Length != 0)
            {
                if (Vector3.Distance(transform.position, _points[_currentPoint].position) < 1.0f)
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
        }

        if (canAttack == false) pouse += Time.deltaTime;
        if (pouse > attackPouse) canAttack = true;

        // атака
        if (canAttack && target != null && attackPower != 0 && Vector3.Distance(transform.position, target.transform.position) < attackdistance)
        {
            
            
            
            GetComponent<Animator>().SetTrigger("Attak");
            target.GetComponent<Health>()._health -= attackPower;
            canAttack = false;
            pouse = 0;
        }
        
        
        
    }
}
