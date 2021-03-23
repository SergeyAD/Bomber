using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMove : MonoBehaviour
{
    public float speedRun;
    public float speedTurn;
    public GameObject guns;
    public GameObject bullet;
    public float pouseFire;
    public float powerFire;
    public int ammor;
    public List<GameObject> keys;
    public Image healthBar;
    public TrajectoryScript Trajectory;
    public GameObject gamePointsText;

    public Camera _mainCamera;
    public Camera _fireCamera;

    public int gamePoints;

    //public float _maxHeight;
    //public float _minHeight;

    private Vector3 _vector;
    private float _turn;
    private float _pouse;
    private bool _canFire = true;


    private void Start()
    {
        keys = new List<GameObject>();
        _mainCamera.enabled = true;
        _fireCamera.enabled = false;
    }


    private void Update()
    {
        
        _vector.z = Input.GetAxis("Vertical");
        _turn = Input.GetAxis("Horizontal");

        // переключение между камерами
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
        
        //обновление здоровья игрока
        healthBar.fillAmount = GetComponent<Health>()._health / 10;
        //обновление счета на экране
        gamePointsText.gameObject.GetComponent<Text>().text = gamePoints.ToString();

    }



    private void FixedUpdate()
    {
        
        // Движение игрока
        var _move = _vector * speedRun * Time.deltaTime;
        transform.parent.gameObject.transform.Translate(_move);
        var turn = _turn * speedTurn * Time.deltaTime;
        transform.parent.gameObject.transform.Rotate(new Vector3(0, turn, 0));

        // Изменение угла выстрела
        if (Input.GetKey(KeyCode.L)) //&& _guns.transform.GetChild(0).gameObject.transform.rotation.x < _maxHeight
        {
            gunRotate(guns.transform, -10);
        }
        else if (Input.GetKey(KeyCode.O)) //&& _guns.transform.GetChild(0).gameObject.transform.rotation.x > _minHeight
        {
            gunRotate(guns.transform, 10);
        }

        // реализация паузы при стрельбе
        if (_canFire == false)
            _pouse += Time.deltaTime;
        if (_pouse > pouseFire)
            _canFire = true; 


        // отрисовка траектории выстрела
        if (_canFire && _fireCamera.enabled )
        {
            Trajectory.ShowTrajectory(guns.transform.GetChild(1).gameObject.transform.position, guns.transform.GetChild(1).gameObject.transform.forward * powerFire);
        }
        else
        {
            Trajectory.HideTrajectory();
        }


        // выстрел
        if (Input.GetKey(KeyCode.Space))
        {
            if (_canFire == true && ammor > 0)
            { 
            
                GameObject bull = Instantiate(bullet, guns.transform.GetChild(1).gameObject.transform.position, transform.rotation);
                bull.GetComponent<BulletMove>()._sender = gameObject;
                bull.GetComponent<Rigidbody>().AddForce(guns.transform.GetChild(1).gameObject.transform.forward*powerFire, ForceMode.Impulse);
                ammor--;
                _canFire = false;
                _pouse = 0;
            }
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<UserPoint>())
        {
            onUserPoint(other);
        }
    
    }

    // изменение угла наклона ствола
    private void gunRotate(Transform _gunTransform, int _rotate)
    {
        _gunTransform.GetChild(1).gameObject.transform.Rotate(new Vector3(0, _rotate * speedRun * Time.deltaTime, 0));
    }

    // обработка использования user point
    private void onUserPoint(Collider _object)
    {
        var _userPoint = _object.transform.GetComponent<UserPoint>();

        // забрать патроны
        ammor += _userPoint._ammor;
        _object.transform.GetComponent<UserPoint>()._ammor = 0;
        // забрать жизни
        transform.GetComponent<Health>()._lives += _userPoint._live;
        _object.transform.GetComponent<UserPoint>()._live = 0;
        // забрать ключи
        if (_userPoint._keys.Count != 0)
        {
            var keys1 = _userPoint._keys;
            foreach (GameObject _obj in keys1)
            {
                keys.Add(_obj);

                _object.transform.GetComponent<UserPoint>()._keys.Remove(_obj);
            }


        }

        // генерация противноков
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

                if (keys.Contains(door.GetComponent<DoorScript>().key))
                {
                    door.GetComponent<DoorScript>().open = true;
                    keys.Remove(door.GetComponent<DoorScript>().key);
                    _object.transform.GetComponent<UserPoint>()._doorToOpen.Remove(door);
                }
            }
        }
        // если все предметы из userPoint забрали, userPoint убираем
        if (userPointEmpty(_userPoint))
        {
            _object.gameObject.SetActive(false);
            Destroy(_object.gameObject);
        }
    }

    private bool userPointEmpty(UserPoint _point)
    {
        return (_point._ammor == 0 && _point._live == 0 && _point._keys.Count == 0 && _point._doorToOpen.Count == 0);
    }

}



