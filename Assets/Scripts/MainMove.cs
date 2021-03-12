using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMove : MonoBehaviour
{
    public float _speedRun;
    public float _speedTurn;
    public GameObject _guns;
    public GameObject _bull;
    public float _pouseFire;
    public float _powerFire;
    public int _ammor;
    //public float _maxHeight;
    //public float _minHeight;

    private Vector3 _vector;
    private float _turn;
    private float _pouse;
    private bool _canFire = true;
    

    private void Update()
    {
        
        _vector.z = Input.GetAxis("Vertical");
        _turn = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        var _move = _vector * _speedRun * Time.deltaTime;
        transform.parent.gameObject.transform.Translate(_move);
        var turn = _turn * _speedTurn * Time.deltaTime;
        transform.parent.gameObject.transform.Rotate(new Vector3(0, turn, 0));

        
        if (Input.GetKey(KeyCode.O)) //&& _guns.transform.GetChild(0).gameObject.transform.rotation.x < _maxHeight
        {
            _guns.transform.GetChild(0).gameObject.transform.Rotate(new Vector3(0, -10 * _speedRun * Time.deltaTime, 0));
            _guns.transform.GetChild(1).gameObject.transform.Rotate(new Vector3(0, -10 * _speedRun * Time.deltaTime, 0));
        }
        else if (Input.GetKey(KeyCode.L)) //&& _guns.transform.GetChild(0).gameObject.transform.rotation.x > _minHeight
        {
            _guns.transform.GetChild(0).gameObject.transform.Rotate(new Vector3(0, 10 * _speedRun * Time.deltaTime, 0));
            _guns.transform.GetChild(1).gameObject.transform.Rotate(new Vector3(0, 10 * _speedRun * Time.deltaTime, 0));
        }

        if (_canFire == false) _pouse += Time.deltaTime;
        if (_pouse > _pouseFire) _canFire = true;

        if (Input.GetKey(KeyCode.Space))
        {
            if (_canFire == true && _ammor > 0)
            { 
            
            GameObject bull = Instantiate(_bull, _guns.transform.GetChild(1).gameObject.transform.position, transform.rotation);
            bull.GetComponent<Rigidbody>().AddForce(_guns.transform.GetChild(1).gameObject.transform.forward*_powerFire, ForceMode.Impulse);
                _ammor--;
                _canFire = false;
                _pouse = 0;
            }
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        _ammor += other.transform.GetComponent<UserPoint>()._ammor;
        other.transform.GetComponent<UserPoint>()._ammor = 0;
        
        // По какой то причине данный кусок кода запускается несколько раз при заходе на UserPoint создается несколько противников вместо одного
        GameObject[] _ePoints = other.transform.GetComponent<UserPoint>()._enemyPointsToActivate;

        for (var i = 0; i < _ePoints.Length; i++)
        {
            Instantiate(_ePoints[i].transform.GetComponent<EnemyPointScript>()._enemy, _ePoints[i].transform.position, _ePoints[i].transform.rotation);
            
        }
        other.gameObject.SetActive(false);
        Destroy(other.gameObject);

    }




}



