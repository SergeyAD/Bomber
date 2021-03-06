using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public static PlayerManager instance = null; // Экземпляр объекта
    public static int gamePoints = 0; 



    private void Awake()
    {
        if (instance == null)
        { 
            instance = this; 
        }
        else if (instance == this)
        { 
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        InitializeManager();
    }

    private void InitializeManager()
    {
        gamePoints = 0;
    }

    public static void ChangeGamePoint(int _change)
    {
        gamePoints += _change;
    }

    public static int GamePoint()
    {
        return (gamePoints);
    }
}


