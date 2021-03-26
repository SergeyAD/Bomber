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
    public GameObject bullet2point;
    public GameObject bullet2;
    public float bullet2pouse;
    public float pouseFire;
    public float powerFire;
    public int ammor;
    public List<GameObject> keys;
    public Image healthBar;
    public TrajectoryScript Trajectory;
    public GameObject gamePointsText;
    public GameObject finish;
    public int gamePoints;

    private Vector3 _vector;
    private float _turn;
    private float _pouse;
    private bool _canFire = true;


    private void Start()
    {
        keys = new List<GameObject>();
    }


    private void Update()
    {
        
        _vector.z = Input.GetAxis("Vertical");
        _turn = Input.GetAxis("Horizontal");
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
             GetComponent<Animator>().SetBool("Forward", true);
        else
            GetComponent<Animator>().SetBool("Forward", false);

        //обновление здоровья игрока
        healthBar.fillAmount = GetComponent<Health>()._health / 10;
        //обновление счета на экране
        gamePointsText.gameObject.GetComponent<Text>().text = gamePoints.ToString();

        // если здоровья 0 - завершение игры
        if (GetComponent<Health>()._health <=0)
        {
            finish.gameObject.GetComponent<UserFinishScript>().endLevel(false);
        }

    }



    private void FixedUpdate()
    {
        
        // Движение игрока
        
        var _move = _vector * speedRun * Time.deltaTime;
        var turn = _turn * speedTurn * Time.deltaTime;
        
        transform.parent.gameObject.transform.Translate(_move);
        transform.parent.gameObject.transform.Rotate(new Vector3(0, turn, 0));

        // Изменение угла выстрела
        if (Input.GetKey(KeyCode.O)) //&& _guns.transform.GetChild(0).gameObject.transform.rotation.x < _maxHeight
        {
            gunRotateUp(guns.transform, -10);
        }
        else if (Input.GetKey(KeyCode.L)) //&& _guns.transform.GetChild(0).gameObject.transform.rotation.x > _minHeight
        {
            gunRotateUp(guns.transform, 10);
        }

        // реализация паузы при стрельбе
        if (_canFire == false)
            _pouse += Time.deltaTime;
        if (_pouse > pouseFire)
            _canFire = true; 


        // отрисовка траектории выстрела
        
        //if (_canFire && _fireCamera.enabled )
        //{
        //    Trajectory.ShowTrajectory(guns.transform.GetChild(1).gameObject.transform.position, guns.transform.GetChild(1).gameObject.transform.forward * powerFire);
        //}
        //else
        //{
        //    Trajectory.HideTrajectory();
        //}


        // выстрел
        if (Input.GetKey(KeyCode.Space))
        {
            
            if (_canFire == true && ammor > 0)
            { 
            
                GameObject bull = Instantiate(bullet, guns.transform.gameObject.transform.position, transform.rotation);
                bull.GetComponent<BulletMove>()._sender = gameObject;
                bull.GetComponent<Rigidbody>().AddForce(guns.transform.gameObject.transform.forward*powerFire, ForceMode.Impulse);
                ammor--;
                _canFire = false;
                _pouse = 0;
            }
        }

        //стрельба из автомата - Не получается (((((
        if (Input.GetMouseButton(0) && Camera.main.GetComponent<CameraMove>()._mainCamera == false)
        {

            Vector3 point = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(point);
            Physics.Raycast(ray, out var hit);
            Vector3 point2 = hit.point - bullet2point.transform.position;

            point2.Normalize();
            GameObject _bull2 = Instantiate(bullet2, bullet2point.transform.position, bullet2point.transform.rotation);
            _bull2.GetComponent<Rigidbody>().AddForce(point2 * powerFire * 7, ForceMode.Impulse);
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
    private void gunRotateUp(Transform _gunTransform, int _rotate)
    {
        // _gunTransform.GetChild(1).gameObject.transform.Rotate(new Vector3(0, _rotate * speedRun * Time.deltaTime, 0));
        _gunTransform.gameObject.transform.Rotate(new Vector3(_rotate * speedRun * Time.deltaTime, 0 , 0));
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



