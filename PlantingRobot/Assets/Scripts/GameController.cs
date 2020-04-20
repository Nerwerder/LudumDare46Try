using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private AudioSource audioSource = null;
    private bool music = true;

    public void Start() {
        audioSource = gameObject.GetComponent<AudioSource>();    
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.M)) {
            ToggleBackgroundMusic();
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
