using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PouseScript : MonoBehaviour
{
    [SerializeField]
    public Canvas canvasPouse;
    [SerializeField]
    public bool pouse;

    [SerializeField]
    private Button _buttonRestart;
    [SerializeField]
    private Button _buttonContinue;
    [SerializeField]
    private Button _buttonExit;
    [SerializeField]
    private Slider _sliderlValue;
    [SerializeField]
    private AudioMixer _audioMixer;


    private void Awake()
    {
        canvasPouse.enabled = false;
        pouse = false;
        GamePouse(pouse);
    }

    private void Start()
    {
        _audioMixer.GetFloat("MasterValue", out float _value);
        _sliderlValue.value = _value;
        
        _buttonRestart.onClick.AddListener(GameRestart);
        _buttonContinue.onClick.AddListener(() => GamePouse(false));
        _buttonExit.onClick.AddListener(GameExit);
        _sliderlValue.onValueChanged.AddListener(a => ChangeValue(a));



    }

    private void Update()
    {
        //пауза в игре
        if (Input.GetKeyDown(KeyCode.P))
        {
            
            GamePouse(!pouse);
            
        }
    }

    private void ChangeValue(float _value)
    {
        _audioMixer.SetFloat("MasterValue", _value);
    }

    private void GamePouse(bool _pouse)
    {
        if (_pouse)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
        canvasPouse.enabled = _pouse;
        pouse = !pouse;
    }

    private void GameRestart()
    {
        // перезагружет сцену но игра не перезапускается.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void GameExit()
    {
        Application.Quit();
    }

    


}
