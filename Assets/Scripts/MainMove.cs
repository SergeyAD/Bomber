using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public Image _healthBar;
    public TrajectoryScript Trajectory;
    public GameObject _gamePointsText;

    public Camera _mainCamera;
    public Camera _fireCamera;


    //public float _maxHeight;
    //public float _minHeight;

    private Vector3 _vector;
    private float _turn;
    private float _pouse;
    private bool _canFire = true;
    public int _gamePoints;

    private void Start()
    {
        _keys = new List<GameObject>();
        _mainCamera.enabled = true;
        _fireCamera.enabled = false;
    }


    private void Update()
    {
        
        _vector.z = Input.GetAxis("Vertical");
        _turn = Input.GetAxis("Horizontal");
        _healthBar.fillAmount = GetComponent<Health>()._health / 10;


        if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.CapsLock))
        {
            _mainCamera.enabled = false;
            _fireCamera.enabled = true;
        }
        if (Input.GetMouseButtonUp(1) || Input.GetKeyUp(KeyCode.CapsLock))
        {
            _mainCamera.enabled = true;
            _fireCamera.enabled = false;
        }

        _gamePointsText.gameObject.GetComponent<Text>().text = _gamePoints.ToString();

    }

    private void gunRotate(Transform _gunTransform, int _rotate)
    {
        //_gunTransform.GetChild(0).gameObject.transform.Rotate(new Vector3(0, _rotate * _speedRun * Time.deltaTime, 0));
        _gunTransform.GetChild(1).gameObject.transform.Rotate(new Vector3(0, _rotate * _speedRun * Time.deltaTime, 0));
    }

    private void FixedUpdate()
    {
        var _move = _vector * _speedRun * Time.deltaTime;
        transform.parent.gameObject.transform.Translate(_move);
        var turn = _turn * _speedTurn * Time.deltaTime;
        transform.parent.gameObject.transform.Rotate(new Vector3(0, turn, 0));

        
        if (Input.GetKey(KeyCode.L)) //&& _guns.transform.GetChild(0).gameObject.transform.rotation.x < _maxHeight
        {
            gunRotate(_guns.transform, -10);

        }
        else if (Input.GetKey(KeyCode.O)) //&& _guns.transform.GetChild(0).gameObject.transform.rotation.x > _minHeight
        {
            gunRotate(_guns.transform, 10);

        }

        if (_canFire == false)
            _pouse += Time.deltaTime;
        
        if (_pouse > _pouseFire)
            _canFire = true; 

        if (_canFire && _fireCamera.enabled )
        {
            
            Trajectory.ShowTrajectory(_guns.transform.GetChild(1).gameObject.transform.position, _guns.transform.GetChild(1).gameObject.transform.forward * _powerFire);
        }
        else
        {
            Trajectory.HideTrajectory();
        }

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

            var _userPoint = other.transform.GetComponent<UserPoint>();


            _ammor += _userPoint._ammor;
            other.transform.GetComponent<UserPoint>()._ammor = 0;
            transform.GetComponent<Health>()._lives += _userPoint._live;
            other.transform.GetComponent<UserPoint>()._live = 0;
            if (_userPoint._keys.Count != 0)
            {
                var keys = _userPoint._keys;
                foreach (GameObject _obj in keys)
                {
                    _keys.Add(_obj);
                    
                    other.transform.GetComponent<UserPoint>()._keys.Remove(_obj);
                }

                
            }
            
            // генерация противноков
            // По какой то причине данный кусок кода запускается несколько раз при заходе на UserPoint создается несколько противников вместо одного решилось дезавтивацией перед destroy
            GameObject[] _ePoints = _userPoint._enemyPointsToActivate;

            for (var i = 0; i < _ePoints.Length; i++)
            {
                Instantiate(_ePoints[i].transform.GetComponent<EnemyPointScript>()._enemy, _ePoints[i].transform.position, _ePoints[i].transform.rotation);

            }
            // открывание дверей
            
           if (_userPoint._doorToOpen.Count != 0)
            {
                var doors = _userPoint._doorToOpen;
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

            

            if (_userPoint._ammor == 0 && _userPoint._live == 0 && _userPoint._keys.Count == 0 && _userPoint._doorToOpen.Count == 0)
            {
                
                other.gameObject.SetActive(false);
                Destroy(other.gameObject);
            }
        }
    
    }




}



