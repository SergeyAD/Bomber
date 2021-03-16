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
    public List<GameObject> _keys;

    //public float _maxHeight;
    //public float _minHeight;

    private Vector3 _vector;
    private float _turn;
    private float _pouse;
    private bool _canFire = true;

    private void Start()
    {
        _keys = new List<GameObject>();
    }


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
                bull.GetComponent<BulletMove>()._sender = gameObject;
                bull.GetComponent<Rigidbody>().AddForce(_guns.transform.GetChild(1).gameObject.transform.forward*_powerFire, ForceMode.Impulse);
                _ammor--;
                _canFire = false;
                _pouse = 0;
            }
        }

    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.transform.GetComponent<UserPoint>())
        {

            _ammor += other.transform.GetComponent<UserPoint>()._ammor;
            other.transform.GetComponent<UserPoint>()._ammor = 0;
            transform.GetComponent<Health>()._lives += other.transform.GetComponent<UserPoint>()._live;
            other.transform.GetComponent<UserPoint>()._live = 0;
            if (other.transform.GetComponent<UserPoint>()._keys.Count != 0)
            {
                var keys = other.transform.GetComponent<UserPoint>()._keys;
                foreach (GameObject _obj in keys)
                {
                    _keys.Add(_obj);
                    
                    other.transform.GetComponent<UserPoint>()._keys.Remove(_obj);
                }

                
            }
            
            // генерация противноков
            // По какой то причине данный кусок кода запускается несколько раз при заходе на UserPoint создается несколько противников вместо одного решилось дезавтивацией перед destroy
            GameObject[] _ePoints = other.transform.GetComponent<UserPoint>()._enemyPointsToActivate;

            for (var i = 0; i < _ePoints.Length; i++)
            {
                Instantiate(_ePoints[i].transform.GetComponent<EnemyPointScript>()._enemy, _ePoints[i].transform.position, _ePoints[i].transform.rotation);

            }
            // открывание дверей
            
           if (other.transform.GetComponent<UserPoint>()._doorToOpen.Count != 0)
            {
                var doors = other.transform.GetComponent<UserPoint>()._doorToOpen;
                foreach (var door in doors)
                {
                    
                    if (_keys.Contains(door.GetComponent<DoorScript>()._key))
                    {
                        door.GetComponent<DoorScript>()._open = true;
                        _keys.Remove(door.GetComponent<DoorScript>()._key);
                        other.transform.GetComponent<UserPoint>()._doorToOpen.Remove(door);

                        //var index = other.transform.GetComponent<UserPoint>()._doorToOpen.Find(item => item == door);

                        //other.transform.GetComponent<UserPoint>()._doorToOpen.Find(door)
                    }
                    
                    
                }
            }

            

            if (other.transform.GetComponent<UserPoint>()._ammor == 0 && other.transform.GetComponent<UserPoint>()._live == 0 && other.transform.GetComponent<UserPoint>()._keys.Count == 0 && other.transform.GetComponent<UserPoint>()._doorToOpen.Count == 0)
            {
                
                other.gameObject.SetActive(false);
                Destroy(other.gameObject);
            }
        }
    
    }




}



