using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    bool _pause = false;
    bool _menuState = true;

    bool _helpState = false;

    public GameObject _menuParent;
    public GameObject _menuCam;
    public GameObject _exitButton;
    public GameObject _help;
    public GameObject _money;
    private AudioSource audioSource = null;
    private bool music = true;

    private void Start()
    {
        Time.timeScale = 0.0f;
        _pause = true;
        audioSource = gameObject.GetComponent<AudioSource>();

#if UNITY_WEBGL
        _exitButton.SetActive(false);
#endif
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

        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleBackgroundMusic();
        }
    }



    public void QuitGame()
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
            _money.SetActive(true);
            _menuState = false;
            ChangePauseState();
        }
        else
        {
            _menuParent.SetActive(true);
            _menuCam.SetActive(true);
            _money.SetActive(false);
            _menuState = true;
            ChangePauseState();
        }

    }

    public void ToggleHelp()
    {
        if(_helpState)
        {
            _helpState = false;
            _help.SetActive(false);
        }
        else
        {
            _helpState = true;
            _help.SetActive(true);
        }
    }

    public void ToggleBackgroundMusic() {
        if(music) {
            StopSound();
            music = false;
        } else {
            StartSound();
            music = true;
        }
    }

    public void StopSound() {
        audioSource.Pause();
    }

    public void StartSound() {
        audioSource.Play();
    }
}
