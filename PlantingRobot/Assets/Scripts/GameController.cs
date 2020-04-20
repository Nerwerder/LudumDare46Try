using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    bool _pause = false;

    bool _menuState = true;

    public GameObject _menuParent;
    public GameObject _menuCam;


    private void Start()
    {
        Time.timeScale = 0.0f;
        _pause = true;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) 
        {
            ChangeMenuState();
        }


        if(Input.GetKeyDown(KeyCode.Pause))
        {
            ChangePauseState();
        }
    }



    void QuitGame()
    {
        Application.Quit();
    }


    void ChangePauseState()
    {
        if (_pause)
        {
            Time.timeScale = 1.0f;
            _pause = false;
        }
        else
        {
            Time.timeScale = 0.0f;
            _pause = true;
        }
            
    }


    public void ChangeMenuState()
    {
        if(_menuState)
        {
            _menuParent.SetActive(false);
            _menuCam.SetActive(false);
            _menuState = false;
            ChangePauseState();
        }
        else
        {
            _menuParent.SetActive(true);
            _menuCam.SetActive(true);
            _menuState = true;
            ChangePauseState();
        }
    }
}
