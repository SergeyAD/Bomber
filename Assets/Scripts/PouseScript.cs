using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PouseScript : MonoBehaviour
{
    public Canvas canvasPouse;
    public bool pouse;
    [SerializeField]
    private Button _buttonRestart;
    [SerializeField]
    private Button _buttonContinue;
    [SerializeField]
    private Button _buttonExit;

    private void Awake()
    {
        canvasPouse.enabled = false;
        pouse = false;

    }

    private void Start()
    {
        _buttonRestart.onClick.AddListener(gameRestart);
        _buttonContinue.onClick.AddListener(() => gamePouse(false));
        _buttonExit.onClick.AddListener(gameExit);
    }

    private void Update()
    {
        //пауза в игре
        if (Input.GetKeyDown(KeyCode.P))
        {
            
            gamePouse(!pouse);
            
        }
    }

    private void gamePouse(bool _pouse)
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

    private void gameRestart()
    {
        // перезагружет сцену но игра не перезапускается.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void gameExit()
    {
        Application.Quit();
    }

    


}
